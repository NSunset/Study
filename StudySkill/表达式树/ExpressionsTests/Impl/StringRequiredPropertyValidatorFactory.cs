using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ExpressionsTests
{
    /// <summary>
    /// string类型不能为空验证
    /// </summary>
    public class StringRequiredPropertyValidatorFactory : PropertyValidatorFactoryBase<string>
    {
        protected override IEnumerable<Expression> CreateExpressionCore(CreatePropertyValidatorInput input)
        {
            var required = input.InputProperty.GetCustomAttribute<RequiredAttribute>();
            if (required == null) yield break;

            Expression<Func<string, bool>> checkBoxExp = value => string.IsNullOrEmpty(value);

            yield return ExpressionHelp.CreateValidateExpression(input,
               ExpressionHelp.CreateCheckExpression(typeof(string),
                checkBoxExp,
                requiredErrorExp
                ));
        }

        private static readonly Expression<Func<string, ValidateResult>> requiredErrorExp = name => ValidateResult.Error($"{name}不能为空");
    }
}
