using System;
using System.Collections.Generic;
using System.Text;

namespace CreationDesign.工厂方法模式
{
    /// <summary>
    /// 交通运输通用接口
    /// </summary>
    public interface ITransport
    {
        /// <summary>
        /// 支付金额，方式
        /// </summary>
        void Payment();
    }
}
