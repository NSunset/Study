using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ExpressionsTests
{
    public class StringLengthPropertyValidatorFactory : PropertyValidatorFactoryBase<string>
    {
        protected override IEnumerable<Expression> CreateExpressionCore(CreatePropertyValidatorInput input)
        {
            var minLengthAttribute = input.Property.GetCustomAttribute<MinLengthAttribute>();
            if (minLengthAttribute != null)
            {
                var minLength = minLengthAttribute.Length;
                Expression<Func<string, bool>> checkBoxExp = value => string.IsNullOrEmpty(value) || value.Length < minLength;

                yield return ExpressionHelp.CreateValidateExpression(input,
                   ExpressionHelp.CreateCheckExpression(typeof(string),
                    checkBoxExp,
                    GetLengMinErrExp(minLength)
                    )
                    );
            }
            var maxLengthAttribute = input.Property.GetCustomAttribute<MaxLengthAttribute>();
            if (maxLengthAttribute != null)
            {
                var maxLength = maxLengthAttribute.Length;
                Expression<Func<string, bool>> checkBoxExp = value => string.IsNullOrEmpty(value) || value.Length > maxLength;

                yield return ExpressionHelp.CreateValidateExpression(input,
                   ExpressionHelp.CreateCheckExpression(typeof(string),
                    checkBoxExp,
                    GetLengMaxErrExp(maxLength)
                    )
                    );
            }
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
