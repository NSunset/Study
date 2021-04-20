using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ExpressionsTests
{
    public class IntNotNullPropertyValidatorFactory : PropertyValidatorFactoryBase<int?>
    {
        protected override IEnumerable<Expression> CreateExpressionCore(CreatePropertyValidatorInput input)
        {
            var requiredAttribute = input.Property.GetCustomAttribute<RequiredAttribute>();
            if (requiredAttribute != null)
            {
                Expression<Func<int?, bool>> checkBoxExp = value => value == null;
                yield return ExpressionHelp.CreateValidateExpression(input,
                    ExpressionHelp.CreateCheckExpression(typeof(int?),
                    checkBoxExp,
                    notNullErrorExp
                    ));
            }
        }

        private static readonly Expression<Func<string, ValidateResult>> notNullErrorExp = name => ValidateResult.Error($"{name}不能为NULL");
    }
}
