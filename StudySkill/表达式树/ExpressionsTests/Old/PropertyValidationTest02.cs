using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Linq.Expressions;
using System.Reflection;

namespace ExpressionsTests
{
    public class PropertyValidationTest02
    {
        private const int _count = 10_000;
        private static Func<CreateClaptrapInput, int, ValidateResult> _func;

        //[SetUp]
        public void Init1()
        {
            try
            {
                var finalExpression = CreateCore();
                _func = finalExpression.Compile();
                Expression<Func<CreateClaptrapInput, int, ValidateResult>> CreateCore()
                {

                    ParameterExpression input = Expression.Parameter(typeof(CreateClaptrapInput), "input");
                    PropertyInfo nameProperty = typeof(CreateClaptrapInput).GetProperty(nameof(CreateClaptrapInput.Name));
                    ConstantExpression name = Expression.Constant(nameProperty.Name);
                    MemberExpression value = Expression.Property(input, nameProperty);
                    ParameterExpression minLength = Expression.Parameter(typeof(int), "minLength");

                    LabelTarget returnLabel = Expression.Label(typeof(ValidateResult));
                    ParameterExpression resultExp = Expression.Variable(typeof(ValidateResult));

                    var body = Expression.Block(new[] { resultExp },
                        CreateDefaultResult(),
                        CreateValidateNameRequiredExpression(),
                        CreateValidateNameMinLengthExpression(),
                        Expression.Label(returnLabel, resultExp));

                    Expression<Func<CreateClaptrapInput, int, ValidateResult>> expression = Expression.Lambda<Func<CreateClaptrapInput, int, ValidateResult>>(body, input, minLength);

                    return expression;

                    Expression CreateValidateNameRequiredExpression()
                    {
                        MethodInfo requiredNameMehtod = typeof(PropertyValidationTest02).GetMethod(nameof(PropertyValidationTest02.ValidateNameRequired), new Type[] { typeof(string), typeof(string) });
                        PropertyInfo okProperty = typeof(ValidateResult).GetProperty(nameof(ValidateResult.IsOk));

                        MethodCallExpression callExp = Expression.Call(requiredNameMehtod, name, value);

                        BinaryExpression assignExp = Expression.Assign(resultExp, callExp);

                        MemberExpression okExp = Expression.Property(resultExp, okProperty);
                        UnaryExpression flageExp = Expression.IsFalse(okExp);

                        ConditionalExpression ifThanExp = Expression.IfThen(flageExp, Expression.Return(returnLabel, resultExp));

                        BlockExpression re = Expression.Block(new[] { resultExp }, assignExp, ifThanExp);
                        return re;
                    }

                    Expression CreateValidateNameMinLengthExpression()
                    {
                        MethodInfo nameMinLengthMethod = typeof(PropertyValidationTest02).GetMethod(nameof(PropertyValidationTest02.ValidateStringMinLength), new Type[] { typeof(string), typeof(string), typeof(int) });
                        PropertyInfo okProperty = typeof(ValidateResult).GetProperty(nameof(ValidateResult.IsOk));

                        MethodCallExpression callExp = Expression.Call(nameMinLengthMethod, name, value, minLength);

                        BinaryExpression assignExp = Expression.Assign(resultExp, callExp);

                        MemberExpression okExp = Expression.Property(resultExp, okProperty);
                        UnaryExpression flageExp = Expression.IsFalse(okExp);

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

        [SetUp]
        public void Init()
        {
            try
            {
                Expression<Func<CreateClaptrapInput, int, ValidateResult>> expression = CreateCore();
                _func = expression.Compile();

                Expression<Func<CreateClaptrapInput, int, ValidateResult>> CreateCore()
                {
                    ParameterExpression input = Expression.Parameter(typeof(CreateClaptrapInput), "input");
                    PropertyInfo nameProperty = typeof(CreateClaptrapInput).GetProperty(nameof(CreateClaptrapInput.Name));
                    ConstantExpression name = Expression.Constant(nameProperty.Name);
                    MemberExpression value = Expression.Property(input, nameProperty);                 

                    ConstantExpression notNullExp = Expression.Constant($"{nameProperty.Name}不能为空", typeof(string));
                    ConstantExpression minLengExp = Expression.Constant("{0}长度最小是{1}", typeof(string));
                    MethodInfo stringFormatMethod = typeof(string).GetMethod(nameof(string.Format), new Type[] { typeof(string), typeof(string), typeof(string) });
                    
                    ParameterExpression intExp = Expression.Parameter(typeof(int), "minLength");
                    MethodCallExpression minValueToStringExp = Expression.Call(intExp, typeof(int).GetMethod(nameof(int.ToString), Type.EmptyTypes));
                    
                    MethodCallExpression minValueExp = Expression.Call(stringFormatMethod, minLengExp, name, minValueToStringExp);

                    ParameterExpression resultExp = Expression.Variable(typeof(ValidateResult), "result");

                    LabelTarget returnLabel = Expression.Label(typeof(ValidateResult));

                    BlockExpression body = Expression.Block(
                        new[] { resultExp },
                        CreateDefaultResult(),
                        CreateNameRequestResult(),
                        CreateMinLengthResult(),
                        Expression.Label(returnLabel, resultExp));

                    Expression<Func<CreateClaptrapInput, int, ValidateResult>> exp = Expression.Lambda<Func<CreateClaptrapInput, int, ValidateResult>>(body,
                        input, intExp);

                    return exp;

                    Expression CreateDefaultResult()
                    {
                        MethodInfo okMethodInfo = typeof(ValidateResult).GetMethod(nameof(ValidateResult.Ok));
                        MethodCallExpression okMethodInfoExp = Expression.Call(okMethodInfo);
                        BinaryExpression createDefaultResult = Expression.Assign(resultExp, okMethodInfoExp);
                        return createDefaultResult;
                    }

                    Expression CreateNameRequestResult()
                    {
                        MethodInfo stringIsNull = typeof(string).GetMethod(nameof(string.IsNullOrEmpty), new Type[] { typeof(string) });
                        MethodCallExpression inputNameIsNullExp = Expression.Call(stringIsNull, value);
                        UnaryExpression conditionExp = Expression.IsTrue(inputNameIsNullExp);

                        MethodInfo errorMehtod = typeof(ValidateResult).GetMethod(nameof(ValidateResult.Error), new Type[] { typeof(string) });
                        MethodCallExpression errorExp = Expression.Call(errorMehtod, notNullExp);

                        var assignExp = Expression.Assign(resultExp, errorExp);

                        ConditionalExpression createValidateNameRequiredExpression = Expression.IfThen(conditionExp, Expression.Return(returnLabel, resultExp));
                        var re = Expression.Block(new[] { resultExp }, assignExp, createValidateNameRequiredExpression);
                        return re;
                    }

                    Expression CreateMinLengthResult()
                    {
                        MemberExpression inputNameLengthExp = Expression.Property(value, typeof(string).GetProperty(nameof(string.Length)));
                        BinaryExpression inputNameLengthLessThanExp = Expression.LessThan(inputNameLengthExp, intExp);
                        UnaryExpression conditionExp = Expression.IsTrue(inputNameLengthLessThanExp);

                        MethodInfo errorMehtod = typeof(ValidateResult).GetMethod(nameof(ValidateResult.Error), new Type[] { typeof(string) });
                        MethodCallExpression errorExp = Expression.Call(errorMehtod, minValueExp);

                        var assignExp = Expression.Assign(resultExp, errorExp);

                        ConditionalExpression createValidateNameMinLengthExpression = Expression.IfThen(conditionExp, Expression.Return(returnLabel, resultExp));
                        var re = Expression.Block(new[] { resultExp }, assignExp, createValidateNameMinLengthExpression);
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
                        Name = "学海无涯不进则退"
                    };
                    var (isOk, errorMessage) = Validate(input);
                    Assert.IsTrue(isOk);
                    Assert.IsNull(errorMessage);
                }
            }
        }

        public static ValidateResult Validate(CreateClaptrapInput input)
        {
            return _func(input, 3);
        }

        public static ValidateResult ValidateCode(string name, string value, int minLength)
        {
            if (string.IsNullOrEmpty(value))
            {
                return ValidateResult.Error($"{name}不能为空");
            }
            if (value.Length < minLength)
            {
                return ValidateResult.Error($"{name}长度最小是{minLength}");
            }
            return ValidateResult.Ok();
        }

        public static ValidateResult ValidateNameRequired(string name, string value)
        {
            return string.IsNullOrEmpty(value) ?
                ValidateResult.Error($"{name}不能为空")
                :
                ValidateResult.Ok();
        }

        public static ValidateResult ValidateStringMinLength(string name, string value, int minLength)
        {
            return value.Length < minLength ?
                ValidateResult.Error($"{name}长度最小是{minLength}")
                :
                ValidateResult.Ok();
        }
        public class CreateClaptrapInput
        {
            public string Name { get; set; }
        }
    }
}
