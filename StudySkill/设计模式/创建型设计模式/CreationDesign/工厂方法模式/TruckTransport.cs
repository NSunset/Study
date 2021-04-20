using System;
using System.Collections.Generic;
using System.Text;

namespace CreationDesign.工厂方法模式
{
    /// <summary>
    /// 货车运输
    /// </summary>
    public class TruckTransport : ITransport
    {
        public void Payment()
        {
            Console.WriteLine("货车运输支付费用");
        }
    }
}
