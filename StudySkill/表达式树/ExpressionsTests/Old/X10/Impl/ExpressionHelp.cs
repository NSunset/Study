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
            ConstantExpression nameExp = Expression.Constant(input.InputProperty.Name, typeof(string));

            UnaryExpression inputValueExp = Expression.Convert(input.InputValueExpression, input.InputType);
            MemberExpression valueExp = Expression.Property(inputValueExp, input.InputProperty);

            InvocationExpression result = Expression.Invoke(validateFuncExpression(input.InputType), nameExp, valueExp, inputValueExp);
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
            ConstantExpression nameExp = Expression.Constant(input.InputProperty.Name, typeof(string));

            UnaryExpression inputValueExp = Expression.Convert(input.InputValueExpression, input.InputType);
            MemberExpression valueExp = Expression.Property(inputValueExp, input.InputProperty);

            InvocationExpression result = Expression.Invoke(validateFuncExpression(input.InputType), nameExp, valueExp, inputValueExp);

            ParameterExpression resultVarExp = Expression.Variable(typeof(ValidateResult));
            BinaryExpression setValidateValueExp = Expression.Assign(resultVarExp, result);
            BinaryExpression setInputValueExp = Expression.Assign(input.ResultExpression, resultVarExp);

            PropertyInfo isOk = typeof(ValidateResult).GetProperty(nameof(ValidateResult.IsOk));

            //如果验证结果为false，直接return验证结果，不往下验证了
            ConditionalExpression ifThenExp = Expression.IfThen(
                 Expression.IsFalse(Expression.Property(resultVarExp, isOk)),
                 Expression.Return(input.ReturnLabel, input.ResultExpression)
                 );

            BlockExpression re = Expression.Block(new[] { resultVarExp }, setValidateValueExp, setInputValueExp, ifThenExp);
            return re;
        }

        /// <summary>
        /// 添加验证表达式
        /// </summary>
        /// <param name="input">属性验证对象:ValidatorAll为true时验证所有表达式，为false时验证出错时return</param>
        /// <param name="validateFuncExpression">返回创建检查表达式的委托</param>
        /// <returns></returns>
        public static Expression CreateValidateExpression(
            CreatePropertyValidatorInput input,
            Func<Type, Expression> validateFuncExpression)
        {
            if (input.ValidatorAll)
                return CreateValidateAllExpression(input, validateFuncExpression);
            else
                return CreateValidateStopExpression(input, validateFuncExpression);

        }

        /// <summary>
        /// 创建检查表达式
        /// </summary>
        /// <param name="valueType">
        /// 验证属性类型
        /// </param>
        /// <param name="checkBoxFactory">
        /// 属性验证表达式:约定输入验证对象类型的表达式，返回具体的属性验证表达式
        /// </param>
        /// <param name="errorMsgFactory">
        /// 错误信息表达式:约定输入验证对象类型的表达式,返回具体的属性验证错误信息表达式
        /// </param>
        /// <returns></returns>
        public static Func<Type, Expression> CreateCheckExpression(
            Type valueType,
            Func<Expression, Expression> checkBoxFactory,
            Func<Expression, Expression> errorMsgFactory)
        {
            return inputType =>
             {
                 //验证属性的名称
                 ParameterExpression nameExp = Expression.Parameter(typeof(string), "name");
                 //验证属性的值
                 ParameterExpression valueExp = Expression.Parameter(valueType, "value");
                 //验证对象的类型
                 ParameterExpression inputExp = Expression.Parameter(inputType, "inputExp");

                 //执行验证表达式
                 Expression checkFactory = checkBoxFactory.Invoke(inputExp);
                 InvocationExpression checkBoxExp = Expression.Invoke(checkFactory, valueExp);

                 //执行错误信息表达式
                 Expression errorFactory = errorMsgFactory.Invoke(inputExp);
                 InvocationExpression errorMsgExp = Expression.Invoke(errorFactory, nameExp);

                 //定义正确的验证结果
                 MethodCallExpression okExp = Expression.Call(typeof(ValidateResult),
                     nameof(ValidateResult.Ok),
                     null);

                 //定义变量，验证结果
                 ParameterExpression resultExp = Expression.Variable(typeof(ValidateResult));

                 //如果验证表达式结果为false，把错误信息表达式结果给验证结果变量。否则把正确验证结果给验证结果变量
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

        /// <summary>
        /// 创建检查表达式
        /// </summary>
        /// <param name="valueType">验证类型</param>
        /// <param name="checkBoxExp">验证表达式</param>
        /// <param name="errorExp">错误信息表达式</param>
        /// <returns></returns>
        public static Func<Type, Expression> CreateCheckExpression(
            Type valueType,
            Expression checkBoxExp,
            Expression errorExp) =>
            CreateCheckExpression(valueType, x => checkBoxExp, x => errorExp);
    }
}
