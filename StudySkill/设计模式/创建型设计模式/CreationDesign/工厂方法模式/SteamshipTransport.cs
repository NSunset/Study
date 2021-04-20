using System;
using System.Collections.Generic;
using System.Text;

namespace CreationDesign.工厂方法模式
{
    /// <summary>
    /// 轮船运输
    /// </summary>
    public class SteamshipTransport : ITransport
    {
        public void Payment()
        {
            Console.WriteLine("轮船运输支付费用");
        }
    }
}
