using System;
using System.Collections.Generic;
using System.Text;

namespace CreationDesign.工厂方法模式
{
    public class SteamshipCreator : Creator
    {
        protected override ITransport GetTransport()
        {
            return new SteamshipTransport();
        }

        public override void Operating()
        {
            base.Operating();
        }
    }
}
