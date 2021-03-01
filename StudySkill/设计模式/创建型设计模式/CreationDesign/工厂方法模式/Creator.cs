using System;
using System.Collections.Generic;
using System.Text;

namespace CreationDesign.工厂方法模式
{
    public abstract class Creator
    {
        protected abstract ITransport GetTransport();

        public void Operating()
        {
            ITransport transport = GetTransport();
            transport.Payment();
        }
    }
}
