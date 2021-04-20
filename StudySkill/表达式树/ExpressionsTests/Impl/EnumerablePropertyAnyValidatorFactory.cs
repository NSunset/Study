using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ExpressionsTests
{
    public class EnumerablePropertyAnyValidatorFactory : EnumerablePropertyValidatorFactoryBase
    {
        protected override IEnumerable<Expression> CreateExpressionCore(CreatePropertyValidatorInput input, Type iEnumerable)
        {
            ParameterExpression valueExp = Expression.Parameter(iEnumerable);
            BinaryExpression leftExp = Expression.Equal(valueExp, Expression.Constant(null));

            MethodCallExpression anyExp = Expression.Call(typeof(Enumerable), nameof(Enumerable.Any), new Type[] { @iEnumerable.GenericTypeArguments[0] }, valueExp);
            Expression rightExp = Expression.IsFalse(anyExp);
            BinaryExpression testExp = Expression.OrElse(leftExp, rightExp);

            Type funcType = Expression.GetFuncType(iEnumerable, typeof(bool));
            var checkBoxExp = Expression.Lambda(funcType, testExp, valueExp);

            yield return ExpressionHelp.CreateValidateExpression(input,
                ExpressionHelp.CreateCheckExpression(iEnumerable,
                checkBoxExp,
                anyErrorExp
                ));
        }

        private static readonly Expression<Func<string, ValidateResult>> anyErrorExp = name => ValidateResult.Error($"{name}必须包含至少一个元素");
    }
}
