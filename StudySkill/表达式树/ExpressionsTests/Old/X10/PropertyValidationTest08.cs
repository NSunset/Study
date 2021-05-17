using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using System.Reflection;
using AgileObjects.ReadableExpressions;

namespace ExpressionsTests
{
    public class PropertyValidationTest08
    {
        private const int _count = 10_000;

        private IValidatorFactory _factory = null;

        [SetUp]
        public void Init()
        {
            try
            {
                IEnumerable<IPropertyValidatorFactory> propertyValidatorFactories = new List<IPropertyValidatorFactory>
                {
                    new StringRequiredPropertyValidatorFactory(),
                    new StringLengthPropertyValidatorFactory(),
                    new IntPropertyValidatorFactory(),
                    new EnumerablePropertyAnyValidatorFactory(),
                    new EnumerablePropertyToArryOrListValidatorFactor(),
                    new IntNotNullPropertyValidatorFactory(),
                    new EnumPropertyValidatorFactory(),
                    new StringEqualPropertyValidatorFactory()
                };
                _factory = new ValidatorFactory(propertyValidatorFactories);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Test]
        public void Run()
        {
            for (int i = 0; i < _count; i++)
            {
                //test 1
                {
                    var input = new CreateClaptrapInput();
                    var (isOk, errorMessage) = Validate(input);
                    NUnit.Framework.Assert.IsFalse(isOk);
                    Assert.IsNotNull(errorMessage);
                }

                //test 2
                {
                    var input = new CreateClaptrapInput
                    {
                        Name = "1"
                    };
                    var (isOk, errorMessage) = Validate(input);
                    Assert.IsFalse(isOk);
                    Assert.IsNotNull(errorMessage);
                }

                //test 3
                {
                    var input = new CreateClaptrapInput
                    {
                        Name = "学海无涯不进则退",
                        NickName = "人人都知道的道理",
                        Ids = new int[] { 1 },
                        Ids1 = new List<int> { 3 }
                    };
                    var (isOk, errorMessage) = Validate(input);
                    Assert.IsTrue(isOk, errorMessage);
                    Assert.IsNull(errorMessage);
                }

                //test 4
                {
                    var input = new CreateClaptrapInput
                    {
                        Name = "小旋风",
                        NickName = "金角大王",
                        Age = 200,
                        Ids = new int[] { 1 },
                        Ids1 = new List<int> { 3 }
                    };
                    var (isOk, errorMessage) = Validate(input);
                    Assert.IsTrue(isOk);
                    Assert.IsNull(errorMessage);
                }

                //test 5
                {
                    var input = new CreateClaptrapInput
                    {
                        Name = "小旋风",
                        NickName = "金角大王",
                        Age = 200,
                        Ids = new int[] { 1 },
                        Ids1 = new List<int> { 3 }
                    };
                    var (isOk, errorMessage) = Validate(input);
                    Assert.IsTrue(isOk, errorMessage);
                    Assert.IsNull(errorMessage);
                }

                //test 6
                {
                    var input = new CreateClaptrapInput
                    {
                        Name = "小旋风",
                        NickName = "金角",
                        Age = 200,
                        Ids = Array.Empty<int>(),
                        Ids1 = new List<int>()
                    };
                    var (isOk, errorMessage) = Validate(input);
                    Assert.False(isOk);
                    Assert.NotNull(errorMessage);
                }

                //test 7
                {
                    var input = new CreateClaptrapInput
                    {
                        Name = "小旋风",
                        NickName = "银角大王",
                        Age = 200,
                        Ids = new int[] { 2 },
                        Ids1 = new List<int> { 1 },
                        Item = Enumerable.Range(0, 10)
                    };
                    var (isOk, errorMessage) = Validate(input);
                    Assert.False(isOk);
                    Assert.NotNull(errorMessage);
                }

                //test 8
                {
                    var input = new CreateClaptrapInput
                    {
                        Name = "小旋风",
                        NickName = "银角大王",
                        Age = 200,
                        Ids = new int[] { 2 },
                        Ids1 = new List<int> { 1 },
                        Number = null
                    };
                    var (isOk, errorMessage) = Validate(input);
                    Assert.False(isOk);
                    Assert.NotNull(errorMessage);
                }

                //test 9
                {
                    var input = new CreateClaptrapInput
                    {
                        Name = "小旋风",
                        NickName = "银角大王",
                        Age = 200,
                        Ids = new int[] { 2 },
                        Ids1 = new List<int> { 1 },
                        InputSex = (Sex)10
                    };
                    var (isOk, errorMessage) = Validate(input);
                    Assert.False(isOk);
                    Assert.NotNull(errorMessage);
                }

                //test 10
                {
                    var input = new CreateClaptrapInput
                    {
                        Name = "小旋风",
                        NickName = "银角大王",
                        Age = 200,
                        Ids = new int[] { 2 },
                        Ids1 = new List<int> { 1 },
                        NumberOfMeals = 10
                    };
                    var (isOk, errorMessage) = Validate(input);
                    Assert.False(isOk);
                    Assert.AreEqual(errorMessage, $"{nameof(input.NumberOfMeals)}要大于{nameof(input.Age)},但是当前{nameof(input.NumberOfMeals)}为{input.NumberOfMeals},{nameof(input.Age)}为{input.Age}");
                }

                //test 11
                {
                    CreateClaptrapInput input = null;
                    var (isOk, errorMessage, allErrorMessage) = Validate(input);
                    Assert.IsFalse(isOk);
                    Assert.NotNull(errorMessage);
                }

                //test 12
                {
                    CreateClaptrapInput input = new CreateClaptrapInput
                    {
                        Name = "小旋风",
                        NickName = "银角大王",
                        Pwd = "123456",
                        OldPwd = "v123456"
                    };
                    var (isOk, errorMessage) = Validate(input);
                    Assert.IsFalse(isOk);
                    Assert.NotNull(errorMessage);
                }

                //test 12
                {
                    CreateClaptrapInput input = new CreateClaptrapInput
                    {
                        Name = "小旋风",
                        NickName = "银角大王",
                        Pwd = "123456",
                        OldPwd = "123456",
                        User = null
                    };
                    var (isOk, errorMessage) = Validate(input);
                    Assert.IsFalse(isOk);
                    Assert.NotNull(errorMessage);
                }
            }
        }

        [Test]
        public void Test()
        {

            ParameterExpression errorMsgResult = Expression.Variable(typeof(List<string>));
            NewExpression newExp = Expression.New(typeof(List<string>));
            var re1 = Expression.Assign(errorMsgResult, newExp);



            Expression Validate(ParameterExpression inputExp)
            {
                var result = Expression.Call(
                                        typeof(ValidateResult),
                                        nameof(ValidateResult.Error),
                                        null,
                                        Expression.Constant("name不能为NULL"));



                PropertyInfo isOk = typeof(ValidateResult).GetProperty(nameof(ValidateResult.IsOk));

                MethodCallExpression valueExp = Expression.Call(
                                                result,
                                                nameof(ValidateResult.GetFirstErrorMessage),
                                                null
                                                );
                MethodCallExpression addValueExp = Expression.Call(
                                                   inputExp,
                                                   nameof(List<string>.Add),
                                                   null,
                                                   valueExp
                                                   );

                ConditionalExpression getValidateResultExp = Expression.IfThen(
                                                    Expression.IsFalse(Expression.Property(result, isOk)),
                                                      addValueExp
                                                    );
                BlockExpression re = Expression.Block(getValidateResultExp);
                return re;
            }



            MethodCallExpression anyExp = Expression.Call(
                typeof(Enumerable),
                nameof(Enumerable.Any),
                new Type[] { typeof(string) },
                errorMsgResult
                );

            Expression validateExp = Validate(errorMsgResult);

            BlockExpression blockExpression = Expression.Block(
                new[] { errorMsgResult },
                re1,
                validateExp,
                anyExp);

            var exp = Expression.Lambda<Func<bool>>(blockExpression);
            var text = exp.ToReadableString();
            var test = exp.Compile()();
            Assert.IsTrue(test);
        }



        public ValidateResult Validate(CreateClaptrapInput input)
        {
            Func<object, ValidateResult> func = _factory.GetValidator(typeof(CreateClaptrapInput));
            return func.Invoke(input);
        }

        public class TestList
        {
            public List<string> Values { get; set; }
        }

    }
}
