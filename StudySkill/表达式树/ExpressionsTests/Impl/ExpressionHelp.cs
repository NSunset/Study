using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Linq;
using NUnit.Framework;
using System.Diagnostics;

namespace ExpressionsTests
{
    public class ExpressionHelp
    {
        /// <summary>
        /// 执行所有验证返回验证结果
        /// </summary>
        /// <param name="input"></param>
        /// <param name="validateFuncExpression"></param>
        /// <returns></returns>
        private static Expression CreateValidateAllExpression(
            CreatePropertyValidatorInput input,
            Func<Type, Expression> validateFuncExpression)
        {
            ConstantExpression nameExp = Expression.Constant(input.Property.Name, typeof(string));

            UnaryExpression inputType = Expression.Convert(input.InputExpression, input.InputType);
            MemberExpression valueExp = Expression.Property(inputType, input.Property);

            InvocationExpression result = Expression.Invoke(validateFuncExpression(input.InputType), nameExp, valueExp, inputType);
            return result;
        }

        /// <summary>
        /// 遇到错误中断reutrn
        /// </summary>
        /// <param name="input"></param>
        /// <param name="validateFuncExpression"></param>
        /// <returns></returns>
        private static Expression CreateValidateStopExpression(
            CreatePropertyValidatorInput input,
            Func<Type, Expression> validateFuncExpression)
        {
            ConstantExpression nameExp = Expression.Constant(input.Property.Name, typeof(string));

            UnaryExpression inputType = Expression.Convert(input.InputExpression, input.InputType);
            MemberExpression valueExp = Expression.Property(inputType, input.Property);

            InvocationExpression result = Expression.Invoke(validateFuncExpression(input.InputType), nameExp, valueExp, inputType);

            ParameterExpression resultVarExp = Expression.Variable(typeof(ValidateResult));
            BinaryExpression setValidateValueExp = Expression.Assign(resultVarExp, result);
            BinaryExpression setInputValueExp = Expression.Assign(input.ResultExpression, resultVarExp);

            PropertyInfo isOk = typeof(ValidateResult).GetProperty(nameof(ValidateResult.IsOk));
            ConditionalExpression ifThenExp = Expression.IfThen(
                 Expression.IsFalse(Expression.Property(resultVarExp, isOk)),
                 Expression.Return(input.ReturnLabel, input.ResultExpression)
                 );

            BlockExpression re = Expression.Block(new[] { resultVarExp }, setValidateValueExp, setInputValueExp, ifThenExp);
            return re;
        }

        public static Expression CreateValidateExpression(
            CreatePropertyValidatorInput input,
            Func<Type, Expression> validateFuncExpression)
        {
            if (input.ValidatorAll)
                return CreateValidateAllExpression(input, validateFuncExpression);
            else
                return CreateValidateStopExpression(input, validateFuncExpression);

        }

        public static Func<Type, Expression> CreateCheckExpression(
            Type valueType,
            Func<Expression, Expression> checkBoxFactory,
            Func<Expression, Expression> errorMsgFactory)
        {
            return inputType =>
             {
                 ParameterExpression nameExp = Expression.Parameter(typeof(string), "name");
                 ParameterExpression valueExp = Expression.Parameter(valueType, "value");

                 ParameterExpression inputExp = Expression.Parameter(inputType, "inputExp");

                 Expression checkFactory = checkBoxFactory.Invoke(inputExp);
                 InvocationExpression checkBoxExp = Expression.Invoke(checkFactory, valueExp);


                 Expression errorFactory = errorMsgFactory.Invoke(inputExp);
                 InvocationExpression errorMsgExp = Expression.Invoke(errorFactory, nameExp);

                 MethodCallExpression okExp = Expression.Call(typeof(ValidateResult),
                     nameof(ValidateResult.Ok),
                     null);

                 ParameterExpression resultExp = Expression.Variable(typeof(ValidateResult));
                 ConditionalExpression body = Expression.IfThenElse(checkBoxExp
                      , Expression.Assign(resultExp, errorMsgExp)
                      , Expression.Assign(resultExp, okExp)
                      );

                 BlockExpression bodyExp = Expression.Block(new[] { resultExp }, body, resultExp);



                 Type type = Expression.GetFuncType(typeof(string), valueType, inputType, typeof(ValidateResult));
                 var exp = Expression.Lambda(type, bodyExp, nameExp, valueExp, inputExp);

                 return exp;
             };
        }

        public static Func<Type, Expression> CreateCheckExpression(
            Type valueType,
            Expression checkBoxExp,
            Expression errorExp) =>
            CreateCheckExpression(valueType, x => checkBoxExp, x => errorExp);
    }
}
