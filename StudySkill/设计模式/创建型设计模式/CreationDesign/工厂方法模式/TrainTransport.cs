using System;
using System.Collections.Generic;
using System.Text;

namespace CreationDesign.工厂方法模式
{
    /// <summary>
    /// 火车运输
    /// </summary>
    public class TrainTransport : ITransport
    {
        public void Payment()
        {
            Console.WriteLine("火车运输支付费用");
        }
    }
}
