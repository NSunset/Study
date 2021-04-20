using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressionsTests
{
    public interface IValidatorFactory
    {
        /// <summary>
        /// 获取验证结果
        /// 一旦遇到错误就直接返回结果不继续往下验证了
        /// </summary>
        /// <param name="type">需要验证对象</param>
        /// <returns></returns>
        Func<object, ValidateResult> GetValidator(Type type);

        /// <summary>
        /// 获取验证结果
        /// 遇到错误也继续往下验证,直到验证完
        /// </summary>
        /// <param name="type">需要验证对象</param>
        /// <returns></returns>
        Func<object, ValidateResult> GetValidatorAll(Type type);
    }
}
