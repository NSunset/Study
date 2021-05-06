using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ExpressionsTests
{
    public class ValidatorDto
    {
        /// <summary>
        /// 需要验证的类型
        /// </summary>
        public Type ValidateType { get; set; }

        /// <summary>
        /// 需要验证的类型的值
        /// </summary>
        public ParameterExpression ValidateTypeValueExp { get; set; }

        /// <summary>
        /// 验证后返回的信息
        /// </summary>
        public ParameterExpression ValidaResultExp { get; set; }

        /// <summary>
        /// 标记返回信息
        /// </summary>
        public LabelTarget ReturnExp { get; set; }
    }
}
