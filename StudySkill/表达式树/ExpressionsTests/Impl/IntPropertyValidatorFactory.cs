using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ExpressionsTests
{
    /// <summary>
    /// int类型验证
    /// </summary>
    public class IntPropertyValidatorFactory : PropertyValidatorFactoryBase<int>
    {
        protected override IEnumerable<Expression> CreateExpressionCore(CreatePropertyValidatorInput input)
        {
            yield return RangeValidator(input);

            yield return GreaterThanValidator(input);
        }

        /// <summary>
        /// 指定范围验证
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private Expression RangeValidator(CreatePropertyValidatorInput input)
        {
            var rangeAttribute = input.InputProperty.GetCustomAttribute<RangeAttribute>();
            if (rangeAttribute == null) return Expression.Empty();

            int min = (int)rangeAttribute.Minimum;
            int max = (int)rangeAttribute.Maximum;
            Expression<Func<int, bool>> checkBoxExp = value => value < min || value > max;
            return ExpressionHelp.CreateValidateExpression(input,
                ExpressionHelp.CreateCheckExpression(typeof(int),
                checkBoxExp,
                GetErrorExp(min, max)
                ));
        }

        /// <summary>
        /// 标记值大于指定字段名的值验证
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private Expression GreaterThanValidator(CreatePropertyValidatorInput input)
        {
            GreaterThanAttribute greaterThanAttribute = input.InputProperty.GetCustomAttribute<GreaterThanAttribute>();
            if (greaterThanAttribute == null) return Expression.Empty();
            Expression CheckBoxExp(Expression inputExp)
            {
                ParameterExpression valueExp = Expression.Parameter(input.InputProperty.PropertyType, "value");

                MemberExpression rightExp = Expression.Property(inputExp, greaterThanAttribute.Target);
                BinaryExpression greaterThanExp = Expression.GreaterThan(valueExp, rightExp);
                UnaryExpression checkExp = Expression.IsFalse(greaterThanExp);

                var er = Expression.Lambda<Func<int, bool>>(checkExp, valueExp);
                return er;
            }

            Expression GetErrorExp(Expression inputExp)
            {
                //$"{nameof(input.NumberOfMeals)}要大于{nameof(input.Age)},但是当前{nameof(input.NumberOfMeals)}为{input.NumberOfMeals},{nameof(input.Age)}为{input.Age}"

                ParameterExpression leftNameExp = Expression.Parameter(typeof(string), "leftName");
                MemberExpression leftValueExp = Expression.Property(inputExp, input.InputProperty);
                ConstantExpression rightNameExp = Expression.Constant(greaterThanAttribute.Target, typeof(string));

                MemberExpression rightValueExp = Expression.Property(inputExp, greaterThanAttribute.Target);

                NewArrayExpression arrExp = Expression.NewArrayInit(
                    typeof(object),
                    Expression.Convert(leftNameExp, typeof(object)),
                    Expression.Convert(rightNameExp, typeof(object)),
                    Expression.Convert(leftValueExp, typeof(object)),
                    Expression.Convert(rightValueExp, typeof(object))
                    );

                MethodInfo stringFormatMethod = typeof(string).GetMethod(nameof(string.Format),
                      new Type[] { typeof(string), typeof(object[]) });

                MethodCallExpression stringFormatExp = Expression.Call(
                    stringFormatMethod,
                    Expression.Constant("{0}要大于{1},但是当前{0}为{2},{1}为{3}", typeof(string)),
                    arrExp);

                MethodCallExpression errorExp = Expression.Call(typeof(ValidateResult),
                     nameof(ValidateResult.Error),
                     null,
                     stringFormatExp
                     );
                var re = Expression.Lambda<Func<string, ValidateResult>>(errorExp, leftNameExp);
                return re;
            }

            return ExpressionHelp.CreateValidateExpression(input,
                ExpressionHelp.CreateCheckExpression(typeof(int),
                CheckBoxExp,
                GetErrorExp
                ));

        }

        private static Expression<Func<string, ValidateResult>> GetErrorExp(int min, int max)
        {
            return name => ValidateResult.Error($"{name}取值范围是{min}-{max}");
        }
    }
}
