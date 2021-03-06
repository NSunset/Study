﻿using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Linq.Expressions;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ExpressionsTests
{
    public class PropertyValidationTest05
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
                            list.Add(CreateValidateStringRequiredExpression(item));
                        }
                        var minLength = item.GetCustomAttribute<MinLengthAttribute>();
                        if (minLength != null)
                        {
                            list.Add(CreateValidateStringMinLengthExpression(item, minLength.Length));
                        }
                    }
                    list.Add(Expression.Label(returnLabel, resultExp));

                    var body = Expression.Block(new[] { resultExp }, list.ToArray());

                    Expression<Func<CreateClaptrapInput, ValidateResult>> expression = Expression.Lambda<Func<CreateClaptrapInput, ValidateResult>>(body, input);

                    return expression;

                    Expression CreateValidateStringRequiredExpression(PropertyInfo property)
                    {
                        ConstantExpression nameExp = Expression.Constant(property.Name, typeof(string));
                        MemberExpression valueExp = Expression.Property(input, property);
                        PropertyInfo isOk = typeof(ValidateResult).GetProperty(nameof(ValidateResult.IsOk));

                        InvocationExpression result = Expression.Invoke(ValidateStringRequiredExp, nameExp, valueExp);
                        BinaryExpression assignExp = Expression.Assign(resultExp, result);

                        UnaryExpression flageExp = Expression.IsFalse(Expression.Property(result, isOk));
                        ConditionalExpression ifThanExp = Expression.IfThen(flageExp, Expression.Return(returnLabel, resultExp));

                        BlockExpression re = Expression.Block(new[] { resultExp }, assignExp, ifThanExp);
                        return re;
                    }

                    Expression CreateValidateStringMinLengthExpression(PropertyInfo property, int minLength)
                    {
                        ConstantExpression nameExp = Expression.Constant(property.Name, typeof(string));
                        MemberExpression valueExp = Expression.Property(input, property);
                        ConstantExpression minLengthExp = Expression.Constant(minLength, typeof(int));
                        PropertyInfo isOk = typeof(ValidateResult).GetProperty(nameof(ValidateResult.IsOk));

                        InvocationExpression result = Expression.Invoke(ValidateStringMinLengthExp, nameExp, valueExp, minLengthExp);
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

        //[SetUp]
        public void Init()
        {
            try
            {
                var expression = CreateCore();
                _func = expression.Compile();

                Expression<Func<CreateClaptrapInput, ValidateResult>> CreateCore()
                {
                    ParameterExpression input = Expression.Parameter(typeof(CreateClaptrapInput), "input");

                    ParameterExpression resultExp = Expression.Variable(typeof(ValidateResult), "result");
                    LabelTarget returnLabel = Expression.Label(typeof(ValidateResult));

                    List<Expression> list = new List<Expression>() { CreateDefaultResult() };

                    foreach (PropertyInfo item in typeof(CreateClaptrapInput).GetProperties())
                    {
                        if (item.GetCustomAttribute<RequiredAttribute>() != null)
                        {
                            list.Add(CreateStringRequestResult(item));
                        }
                        var minLength = item.GetCustomAttribute<MinLengthAttribute>();
                        if (minLength != null)
                        {
                            list.Add(CreateStringMinLengthResult(item, minLength.Length));
                        }
                    }
                    list.Add(Expression.Label(returnLabel, resultExp));

                    BlockExpression body = Expression.Block(new[] { resultExp }, list);

                    Expression<Func<CreateClaptrapInput, ValidateResult>> exp = Expression.Lambda<Func<CreateClaptrapInput, ValidateResult>>(body,
                        input);

                    return exp;

                    Expression CreateDefaultResult()
                    {
                        MethodInfo okMethodInfo = typeof(ValidateResult).GetMethod(nameof(ValidateResult.Ok));
                        MethodCallExpression okMethodInfoExp = Expression.Call(okMethodInfo);
                        BinaryExpression createDefaultResult = Expression.Assign(resultExp, okMethodInfoExp);
                        return createDefaultResult;
                    }

                    Expression CreateStringRequestResult(PropertyInfo property)
                    {
                        ConstantExpression name = Expression.Constant(property.Name);
                        MemberExpression value = Expression.Property(input, property);
                        ConstantExpression notNullExp = Expression.Constant($"{property.Name}不能为空", typeof(string));

                        MethodInfo stringIsNull = typeof(string).GetMethod(nameof(string.IsNullOrEmpty), new Type[] { typeof(string) });
                        MethodCallExpression propertyIsNullExp = Expression.Call(stringIsNull, value);
                        UnaryExpression conditionExp = Expression.IsTrue(propertyIsNullExp);

                        MethodInfo errorMehtod = typeof(ValidateResult).GetMethod(nameof(ValidateResult.Error), new Type[] { typeof(string) });
                        MethodCallExpression errorExp = Expression.Call(errorMehtod, notNullExp);

                        var assignExp = Expression.Assign(resultExp, errorExp);

                        ConditionalExpression createValidateStringRequiredExpression = Expression.IfThen(conditionExp, Expression.Return(returnLabel, resultExp));
                        var re = Expression.Block(new[] { resultExp }, assignExp, createValidateStringRequiredExpression);
                        return re;
                    }

                    Expression CreateStringMinLengthResult(PropertyInfo property, int length)
                    {
                        //ConstantExpression name = Expression.Constant(property.Name);
                        MemberExpression value = Expression.Property(input, property);
                        ConstantExpression minLengExp = Expression.Constant(length, typeof(int));
                        ConstantExpression errorMsgExp = Expression.Constant($"{property.Name}长度最小是{length}", typeof(string));
                        //MethodInfo stringFormatMethod = typeof(string).GetMethod(nameof(string.Format), new Type[] { typeof(string), typeof(string), typeof(string) });


                        //MethodCallExpression minValueToStringExp = Expression.Call(intExp, typeof(int).GetMethod(nameof(int.ToString), Type.EmptyTypes));

                        //MethodCallExpression minValueExp = Expression.Call(stringFormatMethod, minLengExp, name, minValueToStringExp);

                        MemberExpression propertyLengthExp = Expression.Property(value, typeof(string).GetProperty(nameof(string.Length)));
                        BinaryExpression propertyLengthLessThanExp = Expression.LessThan(propertyLengthExp, minLengExp);
                        UnaryExpression conditionExp = Expression.IsTrue(propertyLengthLessThanExp);

                        MethodInfo errorMehtod = typeof(ValidateResult).GetMethod(nameof(ValidateResult.Error), new Type[] { typeof(string) });
                        MethodCallExpression errorExp = Expression.Call(errorMehtod, errorMsgExp);

                        var assignExp = Expression.Assign(resultExp, errorExp);

                        ConditionalExpression createValidateStringMinLengthExpression = Expression.IfThen(conditionExp, Expression.Return(returnLabel, resultExp));
                        var re = Expression.Block(new[] { resultExp }, assignExp, createValidateStringMinLengthExpression);
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

        private static readonly Expression<Func<string, string, ValidateResult>> ValidateStringRequiredExp =
            (name, value) =>
                string.IsNullOrEmpty(value)
                    ? ValidateResult.Error($"{name}不能为空")
                    : ValidateResult.Ok();

        private static readonly Expression<Func<string, string, int, ValidateResult>> ValidateStringMinLengthExp =
            (name, value, minLength) =>
                value.Length < minLength
                    ? ValidateResult.Error($"{name}长度最小是{minLength}")
                    : ValidateResult.Ok();

        public class CreateClaptrapInput
        {
            [Required, MinLength(3)]
            public string Name { get; set; }

            [Required, MinLength(3)]
            public string NickName { get; set; }
        }
    }
}
