using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ExpressionsTests
{
    public interface IPropertyValidatorFactory
    {
        /// <summary>
        /// 创建验证Expression
        /// </summary>
        /// <param name="input">输入参数</param>
        /// <returns></returns>
        IEnumerable<Expression> CreateExpression(CreatePropertyValidatorInput input);
    }
}
