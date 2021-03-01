using System;
using System.Collections.Generic;
using System.Text;

namespace CreationDesign.工厂方法模式
{
    public class TruckCreator : Creator
    {
        protected override ITransport GetTransport()
        {
            return new TruckTransport();
        }
    }
}
