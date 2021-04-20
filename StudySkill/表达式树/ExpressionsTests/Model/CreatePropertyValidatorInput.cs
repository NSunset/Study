using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ExpressionsTests
{
    public class CreatePropertyValidatorInput
    {
        public Expression InputExpression { get; set; }

        public Type InputType { get; set; }

        public PropertyInfo Property { get; set; }

        public LabelTarget ReturnLabel { get; set; }

        public ParameterExpression ResultExpression { get; set; }

        /// <summary>
        /// 是否验证所有:默认不是，一旦遇到错误就直接返回结果不继续往下验证了
        /// </summary>
        public bool ValidatorAll { get; set; }


        public static CreatePropertyValidatorInput GetInstance(Expression inputExp, Type inputType, PropertyInfo inputProperty)
        {
            return new CreatePropertyValidatorInput
            {
                InputExpression = inputExp,
                InputType = inputType,
                Property = inputProperty,
                ValidatorAll = true
            };
        }

        public static CreatePropertyValidatorInput GetInstance(Expression inputExp, Type inputType, PropertyInfo inputProperty, LabelTarget returnLabel, ParameterExpression resultExp)
        {
            return new CreatePropertyValidatorInput
            {
                InputExpression = inputExp,
                InputType = inputType,
                Property = inputProperty,
                ReturnLabel = returnLabel,
                ResultExpression = resultExp
            };
        }
    }


}
