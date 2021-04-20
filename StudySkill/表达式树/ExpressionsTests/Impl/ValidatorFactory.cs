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

        public Func<object, ValidateResult> GetValidator(Type type)
        {
            var result = _validateFunc.GetOrAdd(type, CreateValidator);
            return result;
        }

        public Func<object, ValidateResult> GetValidatorAll(Type type)
        {
            var result = _validateFunc.GetOrAdd(type, CreateValidatorAll);
            return result;
        }

        private Func<object, ValidateResult> CreateValidator(Type type)
        {
            var finalExpression = CreateCore(type);
            return finalExpression.Compile();

            Expression<Func<object, ValidateResult>> CreateCore(Type type)
            {
                ParameterExpression input = Expression.Parameter(typeof(object), "input");

                ParameterExpression inputExp = Expression.Variable(type);
                ParameterExpression validaResultExp = Expression.Variable(typeof(ValidateResult));
                LabelTarget returnExp = Expression.Label(typeof(ValidateResult));

                List<Expression> list = new List<Expression>();

                IEnumerable<Expression> SetDefaultVar()
                {
                    yield return CreateDefaultInput();
                    yield return CreateDefaultResult();
                }


                IEnumerable<Expression> Validator()
                {
                    IEnumerable<Expression> expressions = type.GetProperties().SelectMany(
                        property =>
                            _propertyValidatorFactories.SelectMany(
                                validator =>
                                        validator.CreateExpression(
                                            CreatePropertyValidatorInput.GetInstance(
                                                inputExp,
                                                type,
                                                property,
                                                returnExp,
                                                validaResultExp
                                                ))
                            )
                     );
                    return expressions;
                }

                IEnumerable<Expression> ValidatorHandler()
                {
                    yield return Expression.Label(returnExp, validaResultExp);
                }

                list.AddRange(SetDefaultVar());
                list.AddRange(Validator());
                list.AddRange(ValidatorHandler());

                var body = Expression.Block(new[] { inputExp, validaResultExp }, list.ToArray());

                Expression<Func<object, ValidateResult>> expression = Expression.Lambda<Func<object, ValidateResult>>(body, input);

                return expression;


                Expression CreateDefaultInput()
                {
                    ConditionalExpression inputAssignExp = Expression.IfThenElse(
                     Expression.Equal(input, Expression.Constant(null)),
                     Expression.Assign(inputExp, Expression.New(type)),
                     Expression.Assign(inputExp, Expression.Convert(input, type))
                     );
                    return inputAssignExp;
                }

                Expression CreateDefaultResult()
                {
                    MethodInfo okMethodInfo = typeof(ValidateResult).GetMethod(nameof(ValidateResult.Ok));
                    MethodCallExpression callExpression = Expression.Call(okMethodInfo);
                    var re = Expression.Assign(validaResultExp, callExpression);
                    return re;
                }
            }
        }

        private Func<object, ValidateResult> CreateValidatorAll(Type type)
        {
            var finalExpression = CreateCore(type);
            return finalExpression.Compile();

            Expression<Func<object, ValidateResult>> CreateCore(Type type)
            {
                ParameterExpression input = Expression.Parameter(typeof(object), "input");

                ParameterExpression inputExp = Expression.Variable(type);
                ParameterExpression validaResultExp = Expression.Variable(typeof(ValidateResult));
                ParameterExpression errorMsgResult = Expression.Variable(typeof(List<string>));

                List<Expression> list = new List<Expression>();

                IEnumerable<Expression> SetDefaultVar()
                {
                    yield return CreateDefaultInput();
                    yield return CreateDefaultResult();
                    yield return CreateDefaultErrMsg();
                }

                IEnumerable<Expression> Validator()
                {
                    PropertyInfo isOk = typeof(ValidateResult).GetProperty(nameof(ValidateResult.IsOk));
                    Func<Expression, MethodCallExpression> getErrorMsgFuncExp = validaResultExp => Expression.Call(
                         validaResultExp,
                         nameof(ValidateResult.GetFirstErrorMessage),
                         null
                         );

                    Func<Expression, MethodCallExpression> addErrorValueFuncExp = validaResultExp => Expression.Call(
                            errorMsgResult,
                            nameof(List<string>.Add),
                            null,
                            getErrorMsgFuncExp(validaResultExp)
                            );

                    Func<Expression, ConditionalExpression> ifThanFuncExp = validaResultExp => Expression.IfThen(
                                    Expression.IsFalse(Expression.Property(validaResultExp, isOk)),
                                    addErrorValueFuncExp(validaResultExp)
                                    );

                    foreach (PropertyInfo property in type.GetProperties())
                    {
                        foreach (IPropertyValidatorFactory validator in _propertyValidatorFactories)
                        {
                            IEnumerable<Expression> expressions = validator.CreateExpression(
                                   CreatePropertyValidatorInput.GetInstance(inputExp, type, property));

                            foreach (Expression item in expressions)
                            {
                                BinaryExpression assignExp = Expression.Assign(validaResultExp, item);
                                yield return assignExp;
                                yield return ifThanFuncExp(validaResultExp);
                            }
                        }
                    }
                }

                IEnumerable<Expression> ValidatorHandler()
                {
                    MethodCallExpression errorExp = Expression.Call(typeof(ValidateResult),
                       nameof(ValidateResult.Error),
                       null,
                       errorMsgResult
                       );

                    MethodCallExpression anyExp = Expression.Call(
                         typeof(Enumerable),
                         nameof(Enumerable.Any),
                         new Type[] { typeof(string) },
                         errorMsgResult
                         );

                    ConditionalExpression getValidateResultExp = Expression.IfThen(
                         anyExp,
                         Expression.Assign(validaResultExp, errorExp)
                         );
                    yield return getValidateResultExp;
                    yield return validaResultExp;
                }

                list.AddRange(SetDefaultVar());
                list.AddRange(Validator());
                list.AddRange(ValidatorHandler());

                var body = Expression.Block(new[] { inputExp, validaResultExp, errorMsgResult }, list.ToArray());

                Expression<Func<object, ValidateResult>> expression = Expression.Lambda<Func<object, ValidateResult>>(body, input);

                return expression;


                Expression CreateDefaultInput()
                {
                    ConditionalExpression inputAssignExp = Expression.IfThenElse(
                     Expression.Equal(input, Expression.Constant(null)),
                     Expression.Assign(inputExp, Expression.New(type)),
                     Expression.Assign(inputExp, Expression.Convert(input, type))
                     );
                    return inputAssignExp;
                }

                Expression CreateDefaultResult()
                {
                    MethodInfo okMethodInfo = typeof(ValidateResult).GetMethod(nameof(ValidateResult.Ok));
                    MethodCallExpression callExpression = Expression.Call(okMethodInfo);
                    var re = Expression.Assign(validaResultExp, callExpression);
                    return re;
                }

                Expression CreateDefaultErrMsg()
                {
                    NewExpression newExp = Expression.New(typeof(List<string>));
                    var re1 = Expression.Assign(errorMsgResult, newExp);
                    return re1;
                }
            }
        }
    }
}
