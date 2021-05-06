using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ExpressionsTests
{
    /// <summary>
    /// int可空类型验证
    /// </summary>
    public class IntNotNullPropertyValidatorFactory : PropertyValidatorFactoryBase<int?>
    {
        protected override IEnumerable<Expression> CreateExpressionCore(CreatePropertyValidatorInput input)
        {
            yield return RequiredValidator(input);
        }

        /// <summary>
        /// 值不能等于null验证
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private Expression RequiredValidator(CreatePropertyValidatorInput input)
        {
            var requiredAttribute = input.InputProperty.GetCustomAttribute<RequiredAttribute>();
            if (requiredAttribute == null) return Expression.Empty();
            Expression<Func<int?, bool>> checkBoxExp = value => value == null;
            return ExpressionHelp.CreateValidateExpression(input,
                ExpressionHelp.CreateCheckExpression(typeof(int?),
                checkBoxExp,
                notNullErrorExp
                ));
        }

        private static readonly Expression<Func<string, ValidateResult>> notNullErrorExp = name => ValidateResult.Error($"{name}不能为NULL");
    }
}
