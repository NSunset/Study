using System;
using System.Collections.Generic;
using System.Text;

namespace CreationDesign.工厂方法模式
{
    public abstract class Creator
    {
        /// <summary>
        /// 让子类给予具体的运输方式
        /// </summary>
        /// <returns></returns>
        protected abstract ITransport GetTransport();

        public virtual void Operating()
        {
            ITransport transport = GetTransport();
            transport.Payment();
        }
    }
}
