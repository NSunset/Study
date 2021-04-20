using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace ExpressionsTests
{
    public class PropertyValidationTest06
    {
        private const int _count = 10_000;
        private static Func<CreateClaptrapInput, ValidateResult> _func;

        [SetUp]
        public void Init1()
        {
            try
            {
                var finalExpression = CreateCore();
                _func = finalExpression.Compile();
                Expression<Func<CreateClaptrapInput, ValidateResult>> CreateCore()
                {
                    ParameterExpression input = Expression.Parameter(typeof(CreateClaptrapInput), "input");

                    LabelTarget returnLabel = Expression.Label(typeof(ValidateResult));
                    ParameterExpression resultExp = Expression.Variable(typeof(ValidateResult));

                    List<Expression> list = new List<Expression> { CreateDefaultResult() };
                    foreach (PropertyInfo item in typeof(CreateClaptrapInput).GetProperties().Where(a => a.PropertyType == typeof(string)))
                    {
                        if (item.GetCustomAttribute<RequiredAttribute>() != null)
                        {
                            list.Add(CreateValidate(item, CreateValidateStringRequiredExp()));
                        }
                        var minLength = item.GetCustomAttribute<MinLengthAttribute>();
                        if (minLength != null)
                        {
                            list.Add(CreateValidate(item, CreateValidateStringMinLengthExp(minLength.Length)));
                        }
                    }
                    list.Add(Expression.Label(returnLabel, resultExp));

                    var body = Expression.Block(new[] { resultExp }, list.ToArray());

                    Expression<Func<CreateClaptrapInput, ValidateResult>> expression = Expression.Lambda<Func<CreateClaptrapInput, ValidateResult>>(body, input);

                    return expression;

                    Expression CreateValidate(PropertyInfo property, Expression<Func<string,string,ValidateResult>> expression)
                    {
                        ConstantExpression nameExp = Expression.Constant(property.Name, typeof(string));
                        MemberExpression valueExp = Expression.Property(input, property);
                        PropertyInfo isOk = typeof(ValidateResult).GetProperty(nameof(ValidateResult.IsOk));

                        InvocationExpression result = Expression.Invoke(expression, nameExp, valueExp);
                        BinaryExpression assignExp = Expression.Assign(resultExp, result);

                        UnaryExpression flageExp = Expression.IsFalse(Expression.Property(result, isOk));
                        ConditionalExpression ifThanExp = Expression.IfThen(flageExp, Expression.Return(returnLabel, resultExp));

                        BlockExpression re = Expression.Block(new[] { resultExp }, assignExp, ifThanExp);
                        return re;
                    }

                    Expression CreateDefaultResult()
                    {
                        MethodInfo okMethodInfo = typeof(ValidateResult).GetMethod(nameof(ValidateResult.Ok));
                        MethodCallExpression callExpression = Expression.Call(okMethodInfo);

                        var re = Expression.Assign(resultExp, callExpression);
                        return re;
                    }
                }

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
                        NickName = "人人都知道的道理"
                    };
                    var (isOk, errorMessage) = Validate(input);
                    Assert.IsTrue(isOk);
                    Assert.IsNull(errorMessage);
                }
            }
        }

        public static ValidateResult Validate(CreateClaptrapInput input)
        {
            return _func(input);
        }

        private static Expression<Func<string, string, ValidateResult>> CreateValidateStringRequiredExp()
        {
            return (name, value) =>
                string.IsNullOrEmpty(value)
                    ? ValidateResult.Error($"{name}不能为空")
                    : ValidateResult.Ok();
        }

        private static Expression<Func<string, string, ValidateResult>> CreateValidateStringMinLengthExp(int minLength)
        {
            return (name, value) =>
                value.Length < minLength
                    ? ValidateResult.Error($"{name}长度最小是{minLength}")
                    : ValidateResult.Ok();
        }

        public class CreateClaptrapInput
        {
            [Required, MinLength(3)]
            public string Name { get; set; }

            [Required, MinLength(3)]
            public string NickName { get; set; }
        }
    }
}
