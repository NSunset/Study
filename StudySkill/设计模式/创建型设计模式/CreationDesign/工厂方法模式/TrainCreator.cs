using System;
using System.Collections.Generic;
using System.Text;

namespace CreationDesign.工厂方法模式
{
    public class TrainCreator : Creator
    {
        protected override ITransport GetTransport()
        {
            return new TrainTransport();
        }
    }
}
