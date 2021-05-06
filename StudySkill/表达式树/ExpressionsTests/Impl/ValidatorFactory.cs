using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Linq;
using System.Reflection;
using AgileObjects.ReadableExpressions;

namespace ExpressionsTests
{
    public class ValidatorFactory : IValidatorFactory
    {
        private static readonly ConcurrentDictionary<Type, Func<object, ValidateResult>> _validateFunc = new ConcurrentDictionary<Type, Func<object, ValidateResult>>();

        private readonly IEnumerable<IPropertyValidatorFactory> _propertyValidatorFactories;
        public ValidatorFactory(IEnumerable<IPropertyValidatorFactory> propertyValidatorFactories)
        {
            _propertyValidatorFactories = propertyValidatorFactories;
        }

        /// <summary>
        /// 获取验证所有表达式并返回第一个错误信息
        /// </summary>
        /// <param name="type">需要验证的类型</param>
        /// <returns></returns>
        public Func<object, ValidateResult> GetValidator(Type type)
            => GetValidator(type, CreateValidatorExpression);

        /// <summary>
        /// 获取验证所有表达式并返回所有错误信息
        /// </summary>
        /// <param name="type">需要验证的类型</param>
        /// <returns></returns>
        public Func<object, ValidateResult> GetValidatorAll(Type type)
            => GetValidator(type, CreateValidatorAllExpression);

        private Func<object, ValidateResult> GetValidator(Type type, Func<ValidatorDto, Expression> CalidatorExpression)
        {
            var flage = _validateFunc.TryGetValue(type, out Func<object, ValidateResult> value);
            if (!flage)
            {
                value = CreateValidatorFunc(type, CalidatorExpression);
                var addFlage = _validateFunc.TryAdd(type, value);
                if (!addFlage)
                {
                    throw new ArgumentNullException($"验证字典插入值失败,插入值为:{value}");
                }
            }
            return value;
        }

        /// <summary>
        /// 添加验证所有注册的表达式，记录所有错误信息
        /// </summary>
        /// <param name="validatorDto">验证参数</param>
        /// <returns></returns>
        private Expression CreateValidatorAllExpression(ValidatorDto validatorDto)
        {
            ParameterExpression errorMsgResult = Expression.Variable(typeof(List<string>));
            PropertyInfo isOk = typeof(ValidateResult).GetProperty(nameof(ValidateResult.IsOk));
            List<Expression> list = new List<Expression>();

            //变量设置默认值
            IEnumerable<Expression> SetDefaultVar()
            {
                yield return CreateDefaultErrMsg();
            }

            IEnumerable<Expression> Validator()
            {
                //获取验证结果的错误信息
                Func<Expression, MethodCallExpression> getErrorMsgFuncExp = validaResultExp => Expression.Call(
                     validaResultExp,
                     nameof(ValidateResult.GetFirstErrorMessage),
                     null
                     );

                //errorMsgResult添加错误信息
                Func<Expression, MethodCallExpression> addErrorValueFuncExp = validaResultExp => Expression.Call(
                        errorMsgResult,
                        nameof(List<string>.Add),
                        null,
                        getErrorMsgFuncExp(validaResultExp)
                        );

                //如果验证结果为false，添加错误信息
                Func<Expression, ConditionalExpression> ifThanFuncExp = validaResultExp => Expression.IfThen(
                                Expression.IsFalse(Expression.Property(validaResultExp, isOk)),
                                addErrorValueFuncExp(validaResultExp)
                                );

                //所有属性都走一遍注册的所有表达式验证器
                foreach (PropertyInfo property in validatorDto.ValidateType.GetProperties())
                {
                    foreach (IPropertyValidatorFactory validator in _propertyValidatorFactories)
                    {
                        IEnumerable<Expression> expressions = validator.CreateExpression(
                               CreatePropertyValidatorInput.GetInstance(validatorDto.ValidateTypeValueExp, validatorDto.ValidateType, property));

                        foreach (Expression item in expressions)
                        {
                            if (item.NodeType == ExpressionType.Default)
                                continue;
                            BinaryExpression assignExp = Expression.Assign(validatorDto.ValidaResultExp, item);
                            yield return assignExp;
                            yield return ifThanFuncExp(validatorDto.ValidaResultExp);
                        }
                    }
                }
            }

            IEnumerable<Expression> ValidatorHandler()
            {
                //把errorMsgResult赋值给验证对象，并设置验证对象结果为false
                MethodCallExpression setErrorExp = Expression.Call(validatorDto.ValidaResultExp,
                   nameof(ValidateResult.SetErrorMessage),
                   null,
                   errorMsgResult
                   );

                //查看errorMsgResult是否有值
                MethodCallExpression anyExp = Expression.Call(
                     typeof(Enumerable),
                     nameof(Enumerable.Any),
                     new Type[] { typeof(string) },
                     errorMsgResult
                     );

                //如果errorMsgResult有值，就把设置了错误信息的验证结果赋值为验证对象
                ConditionalExpression getValidateResultExp = Expression.IfThen(
                     anyExp,
                     Expression.Assign(validatorDto.ValidaResultExp, setErrorExp)
                     );
                yield return getValidateResultExp;
            }

            list.AddRange(SetDefaultVar());
            list.AddRange(Validator());
            list.AddRange(ValidatorHandler());

            var body = Expression.Block(new[] { errorMsgResult }, list.ToArray());

            return body;

            //给变量errorMsgResult赋初始值，
            //如果传进来的验证结果为false，就把认证结果的错误信息赋给errorMsgResult
            //如果传进来的验证结果为true,就赋空值
            Expression CreateDefaultErrMsg()
            {
                NewExpression newExp = Expression.New(typeof(List<string>));
                PropertyInfo errorMessage = typeof(ValidateResult).GetProperty(nameof(ValidateResult.ErrorMessage));
                ConditionalExpression re = Expression.IfThenElse(
                     Expression.IsFalse(Expression.Property(validatorDto.ValidaResultExp, isOk)),
                     Expression.Assign(errorMsgResult, Expression.Property(validatorDto.ValidaResultExp, errorMessage)),
                     Expression.Assign(errorMsgResult, newExp)
                     );

                return re;
            }
        }

        /// <summary>
        /// 添加验证所有注册的表达式，记录第一个错误信息
        /// </summary>
        /// <param name="validatorDto">验证参数</param>
        /// <returns></returns>
        private Expression CreateValidatorExpression(ValidatorDto validatorDto)
        {
            //所有属性都走一遍注册的所有表达式验证器
            IEnumerable<Expression> expressions = validatorDto.ValidateType.GetProperties().SelectMany(
                property =>
                    _propertyValidatorFactories.SelectMany(
                        validator =>
                                validator.CreateExpression(
                                    CreatePropertyValidatorInput.GetInstance(
                                        validatorDto.ValidateTypeValueExp,
                                        validatorDto.ValidateType,
                                        property,
                                        validatorDto.ReturnExp,
                                        validatorDto.ValidaResultExp
                                        ))
                    )
             );

            var body = Expression.Block(new[] { validatorDto.ValidaResultExp }, expressions);
            return body;
        }

        /// <summary>
        /// 创建验证委托
        /// </summary>
        /// <param name="inputType">需要验证类型</param>
        /// <param name="CreateCore">验证表达式实现</param>
        /// <returns></returns>
        private Func<object, ValidateResult> CreateValidatorFunc(Type inputType,
                                                                Func<ValidatorDto, Expression> CreateCore)
        {
            var finalExpression = CreateValidateExp(inputType);
            var expString = finalExpression.ToReadableString();
            return finalExpression.Compile();

            Expression<Func<object, ValidateResult>> CreateValidateExp(Type validateType)
            {
                //定义需要验证对象的值
                ParameterExpression inputExp = Expression.Parameter(typeof(object), "input");
                ValidatorDto validatorDto = new ValidatorDto
                {
                    ValidateType = validateType,
                    ValidateTypeValueExp = inputExp,
                    ValidaResultExp = Expression.Variable(typeof(ValidateResult)),
                    ReturnExp = Expression.Label(typeof(ValidateResult))
                };

                //默认验证结果为通过
                Expression CreateDefaultResult()
                {
                    MethodInfo okMethodInfo = typeof(ValidateResult).GetMethod(nameof(ValidateResult.Ok));
                    MethodCallExpression callExpression = Expression.Call(okMethodInfo);
                    var re = Expression.Assign(validatorDto.ValidaResultExp, callExpression);
                    return re;
                }

                //需要验证的对象如果为null就实例化空对象,否则无法验证对象里的属性
                Expression CreateDefaultInput()
                {
                    ConditionalExpression inputAssignExp = Expression.IfThenElse(
                     Expression.Equal(validatorDto.ValidateTypeValueExp, Expression.Constant(null)),
                     Expression.Assign(validatorDto.ValidateTypeValueExp, Expression.New(validatorDto.ValidateType)),
                     Expression.Assign(validatorDto.ValidateTypeValueExp, Expression.Convert(inputExp, validatorDto.ValidateType))
                     );

                    return inputAssignExp;
                }

                //递归验证所有属性和子对象
                IEnumerable<Expression> GetExpression(Type inputType)
                {
                    yield return CreateDefaultResult();
                    yield return CreateDefaultInput();
                    Expression commonPropertyValidate = CreateCore(validatorDto);
                    yield return commonPropertyValidate;

                    foreach (var item in GetChildExpression(inputType, validatorDto.ValidateTypeValueExp))
                    {
                        yield return item;
                    }

                    //验证子对象
                    IEnumerable<Expression> GetChildExpression(Type fatherType, Expression fatherValueExp)
                    {
                        //查找属性有没有继承IValidateObject接口，如果没有就跳出。
                        var validateObjectProperty = fatherType.GetProperties().Where(p => p.PropertyType.GetInterface(nameof(IValidateObject)) != null);

                        if (!validateObjectProperty.Any()) yield break;

                        //继承了IValidateObject约定为子对象验证。进行验证
                        foreach (PropertyInfo property in validateObjectProperty)
                        {
                            //var validateObject = property.PropertyType.GetInterface(nameof(IValidateObject));
                            //if (validateObject == null) yield break;
                            //获取子对象值
                            MemberExpression propertyValue = Expression.Property(
                                Expression.Convert(fatherValueExp, fatherType),
                                property);
                            //获取子对象类型
                            validatorDto.ValidateType = property.PropertyType;
                            yield return Expression.Assign(validatorDto.ValidateTypeValueExp, propertyValue);
                            //如果子对象值为null，实例化空对象赋值,否则无法验证内部属性
                            yield return CreateDefaultInput();
                            yield return CreateCore(validatorDto);

                            foreach (var item in GetChildExpression(property.PropertyType, propertyValue))
                            {
                                yield return item;
                            }
                        }
                    }
                    yield return Expression.Label(validatorDto.ReturnExp, validatorDto.ValidaResultExp);
                }

                var body = Expression.Block(new[] { validatorDto.ValidaResultExp }, GetExpression(inputType));
                Expression<Func<object, ValidateResult>> finalExpression = Expression.Lambda<Func<object, ValidateResult>>(body, inputExp);
                return finalExpression;
            }
        }

    }
}
