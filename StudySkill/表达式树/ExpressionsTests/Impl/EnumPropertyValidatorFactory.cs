using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Linq;

namespace ExpressionsTests
{
    public class EnumPropertyValidatorFactory : IPropertyValidatorFactory
    {
        public IEnumerable<Expression> CreateExpression(CreatePropertyValidatorInput input)
        {
            EnumAttribute enumAttribute = input.Property.GetCustomAttribute<EnumAttribute>();
            if (enumAttribute == null)
            {
                yield break;
            }
            var type = input.Property.PropertyType;
            if (!type.IsEnum)
            {
                yield break;
            }
            var okRange = string.Join(
                "或",
                Enum.GetValues(type).Cast<Enum>().Select(x => x.ToString("D")));
            yield return ExpressionHelp.CreateValidateExpression(input,
                ExpressionHelp.CreateCheckExpression(type,
                GetCheckExp(input),
                GetErrorExp(input)
                ));
        }



        private Func<Expression, Expression> GetCheckExp(CreatePropertyValidatorInput input)
        {
            Type type = input.Property.PropertyType;
            ParameterExpression nameExp = Expression.Parameter(typeof(string), "name");
            ParameterExpression valueExp = Expression.Parameter(type, "value");

            var enumValues = Enum.GetValues(type);
            MethodCallExpression castExp = Expression.Call(
                typeof(Enumerable),
                nameof(Enumerable.Cast),
                new Type[] { type },
                Expression.Constant(enumValues));

            MethodCallExpression containsExp = Expression.Call(
                typeof(Enumerable),
                nameof(Enumerable.Contains),
                new[] { type },
                castExp,
                valueExp
                );

            UnaryExpression flageExp = Expression.IsFalse(containsExp);

            Type expType = Expression.GetFuncType(type, typeof(bool));
            var re = Expression.Lambda(expType, flageExp, valueExp);

            Func<Expression, Expression> x = x => re;
            return x;
        }

        private Func<Expression, Expression> GetErrorExp(CreatePropertyValidatorInput input)
        {
            Func<Expression, Expression> error = inputExp =>
             {
                 //{}只能输入{}不能输入{}
                 Type type = input.Property.PropertyType;
                 var okRange = string.Join(
                    "或",
                    Enum.GetValues(type).Cast<Enum>().Select(x => x.ToString("D")));


                 ParameterExpression nameExp = Expression.Parameter(typeof(string), "name");
                 ConstantExpression okExp = Expression.Constant(okRange, typeof(string));


                 MemberExpression errorExp = Expression.Property(inputExp, input.Property);

                 MethodCallExpression stringFormatExp = Expression.Call(
                  typeof(string),
                  nameof(string.Format),
                  null,
                  Expression.Constant("{0}只能输入{1}不能输入{2}", typeof(string)),
                  Expression.Convert(nameExp, typeof(object)),
                  Expression.Convert(okExp, typeof(object)),
                  Expression.Convert(errorExp, typeof(object))
                  );

                 MethodCallExpression errorExpFactory = Expression.Call(typeof(ValidateResult),
                     nameof(ValidateResult.Error),
                     null,
                     stringFormatExp);

                 var er = Expression.Lambda<Func<string, ValidateResult>>(errorExpFactory, nameExp);
                 return er;
             };

            return error;
        }
    }
}
