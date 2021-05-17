using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ExpressionsTests
{
    /// <summary>
    /// string类型Equal验证,不区分大小写
    /// </summary>
    public class StringEqualPropertyValidatorFactory : PropertyValidatorFactoryBase<string>
    {
        protected override IEnumerable<Expression> CreateExpressionCore(CreatePropertyValidatorInput input)
        {
            var equalToAttribute = input.InputProperty.GetCustomAttribute<EqualToAttribute>();
            if (equalToAttribute == null) yield break;

            Expression CheckExpBoxFactory(Expression inputExp)
            {
                ParameterExpression valueExp = Expression.Parameter(input.InputProperty.PropertyType, "value");
                MemberExpression rightExp = Expression.Property(inputExp, equalToAttribute.Target);


                BinaryExpression leftEqualNullExp = Expression.Equal(valueExp, Expression.Constant(null));
                BinaryExpression rightEqualNullExp = Expression.Equal(rightExp, Expression.Constant(null));
                UnaryExpression orToNullExp = Expression.IsTrue(Expression.OrElse(leftEqualNullExp, rightEqualNullExp));
                UnaryExpression allToNullExp = Expression.IsTrue(Expression.AndAlso(leftEqualNullExp, rightEqualNullExp));

                ParameterExpression resultExp = Expression.Variable(typeof(bool));
                LabelTarget returnExp = Expression.Label(typeof(bool));

                MethodInfo toLower = typeof(string).GetMethod(nameof(string.ToLower), Type.EmptyTypes);
                BinaryExpression notEqualExp = Expression.NotEqual(
                    Expression.Call(valueExp, toLower),
                    Expression.Call(rightExp, toLower));

                ConditionalExpression ifExp = Expression.IfThenElse(
                    allToNullExp,
                     Expression.Assign(resultExp, Expression.Constant(false)),
                     Expression.IfThenElse(
                         orToNullExp,
                         Expression.Assign(resultExp, Expression.Constant(true)),
                         Expression.Assign(resultExp, notEqualExp)
                         )
                    );

                BlockExpression blockExp = Expression.Block(new[] { resultExp }, ifExp, Expression.Label(returnExp, resultExp));

                var checkBoxExp = Expression.Lambda<Func<string, bool>>(blockExp, valueExp);
                return checkBoxExp;
            }

            Expression ErrorExpFactory(Expression inputExp)
            {
                ParameterExpression nameExp = Expression.Parameter(typeof(string), "name");
                ConstantExpression rightExp = Expression.Constant(equalToAttribute.Target, typeof(string));

                MethodCallExpression stringFormatExp = Expression.Call(
                     typeof(string),
                     nameof(string.Format),
                     null,
                     Expression.Constant("{0}不等于{1}", typeof(string)),
                     Expression.Convert(nameExp, typeof(object)),
                     Expression.Convert(rightExp, typeof(object))
                     );

                var errorMsg = Expression.Lambda<Func<string, ValidateResult>>(_getErrorMsgExp(stringFormatExp), nameExp);
                return errorMsg;
            }

            yield return ExpressionHelp.CreateValidateExpression(input,
               ExpressionHelp.CreateCheckExpression(typeof(string),
                CheckExpBoxFactory,
                ErrorExpFactory
                )
                );
        }

        private static readonly Func<Expression, Expression> _getErrorMsgExp =
            x => Expression.Call(
                           typeof(ValidateResult),
                           nameof(ValidateResult.Error),
                           null,
                           x);
    }
}
