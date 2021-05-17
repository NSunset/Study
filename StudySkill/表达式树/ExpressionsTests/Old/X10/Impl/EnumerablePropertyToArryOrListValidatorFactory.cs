using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ExpressionsTests
{
    /// <summary>
    /// Enumerable<T>必须ToArry或者ToList验证
    /// </summary>
    public class EnumerablePropertyToArryOrListValidatorFactor : EnumerablePropertyValidatorFactoryBase
    {
        protected override IEnumerable<Expression> CreateExpressionCore(CreatePropertyValidatorInput input, Type iEnumerable)
        {
            ParameterExpression valueExp = Expression.Parameter(iEnumerable, "value");
            TypeBinaryExpression typeIsExp = Expression.TypeIs(valueExp, typeof(ICollection));
            UnaryExpression falseExp = Expression.IsFalse(typeIsExp);
            Type funcType = Expression.GetFuncType(iEnumerable, typeof(bool));
            LambdaExpression body = Expression.Lambda(funcType, falseExp, valueExp);

            yield return ExpressionHelp.CreateValidateExpression(input,
                ExpressionHelp.CreateCheckExpression(iEnumerable,
                body,
                toArryOrListErrorExp
                )
                );
        }

        private static readonly Expression<Func<string, ValidateResult>> toArryOrListErrorExp = name => ValidateResult.Error($"{name} 必须已经 ToList 或者 ToArray");
    }
}
