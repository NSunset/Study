using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ExpressionsTests
{
    /// <summary>
    /// string类型长度验证
    /// </summary>
    public class StringLengthPropertyValidatorFactory : PropertyValidatorFactoryBase<string>
    {
        protected override IEnumerable<Expression> CreateExpressionCore(CreatePropertyValidatorInput input)
        {
            yield return MinLengthValidator(input);

            yield return MaxLengthValidator(input);
        }

        /// <summary>
        /// 不能小于指定最小值验证
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private Expression MinLengthValidator(CreatePropertyValidatorInput input)
        {
            var minLengthAttribute = input.InputProperty.GetCustomAttribute<MinLengthAttribute>();
            if (minLengthAttribute == null) return Expression.Empty();

            var minLength = minLengthAttribute.Length;
            Expression<Func<string, bool>> checkBoxExp = value => string.IsNullOrEmpty(value) || value.Length < minLength;

            return ExpressionHelp.CreateValidateExpression(input,
              ExpressionHelp.CreateCheckExpression(typeof(string),
               checkBoxExp,
               GetLengMinErrExp(minLength)
               )
               );
        }

        /// <summary>
        /// 不能大于指定最大值验证
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private Expression MaxLengthValidator(CreatePropertyValidatorInput input)
        {
            var maxLengthAttribute = input.InputProperty.GetCustomAttribute<MaxLengthAttribute>();
            if (maxLengthAttribute == null) return Expression.Empty();

            var maxLength = maxLengthAttribute.Length;
            Expression<Func<string, bool>> checkBoxExp = value => string.IsNullOrEmpty(value) || value.Length > maxLength;

            return ExpressionHelp.CreateValidateExpression(input,
              ExpressionHelp.CreateCheckExpression(typeof(string),
               checkBoxExp,
               GetLengMaxErrExp(maxLength)
               )
               );

        }

        private static Expression<Func<string, ValidateResult>> GetLengMinErrExp(int minLength)
        {
            return name => ValidateResult.Error($"{name}长度不能小于{minLength}");
        }

        private static Expression<Func<string, ValidateResult>> GetLengMaxErrExp(int maxLength)
        {
            return name => ValidateResult.Error($"{name}长度不能大于{maxLength}");
        }
    }
}
