using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ExpressionsTests
{
    public class CreatePropertyValidatorInput
    {
        /// <summary>
        /// 需要验证的对象
        /// </summary>
        public Expression InputValueExpression { get; set; }

        /// <summary>
        /// 需要验证的属性类型
        /// </summary>
        public Type InputType { get; set; }

        /// <summary>
        /// 需要验证的属性
        /// </summary>
        public PropertyInfo InputProperty { get; set; }

        /// <summary>
        /// 返回值标签
        /// </summary>
        public LabelTarget ReturnLabel { get; set; }

        /// <summary>
        /// 返回值变量
        /// </summary>
        public ParameterExpression ResultExpression { get; set; }

        /// <summary>
        /// 是否验证所有:默认不是，一旦遇到错误就直接返回结果不继续往下验证了
        /// </summary>
        public bool ValidatorAll { get; set; }

        /// <summary>
        /// 添加实例：指定验证所有
        /// </summary>
        /// <param name="inputValueExp"></param>
        /// <param name="inputType"></param>
        /// <param name="inputProperty"></param>
        /// <returns></returns>
        public static CreatePropertyValidatorInput GetInstance(Expression inputValueExp, Type inputType, PropertyInfo inputProperty)
        {
            return new CreatePropertyValidatorInput
            {
                InputValueExpression = inputValueExp,
                InputType = inputType,
                InputProperty = inputProperty,
                ValidatorAll = true
            };
        }

        /// <summary>
        /// 添加实例：指定不验证所有,一旦遇到错误就直接返回结果不继续往下验证了
        /// </summary>
        /// <param name="inputValueExp"></param>
        /// <param name="inputType"></param>
        /// <param name="inputProperty"></param>
        /// <param name="returnLabel"></param>
        /// <param name="resultExp"></param>
        /// <returns></returns>
        public static CreatePropertyValidatorInput GetInstance(Expression inputValueExp, Type inputType, PropertyInfo inputProperty, LabelTarget returnLabel, ParameterExpression resultExp)
        {
            return new CreatePropertyValidatorInput
            {
                InputValueExpression = inputValueExp,
                InputType = inputType,
                InputProperty = inputProperty,
                ReturnLabel = returnLabel,
                ResultExpression = resultExp
            };
        }
    }


}
