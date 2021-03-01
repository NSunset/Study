using System;
using System.Collections.Generic;
using System.Text;

namespace CreationDesign.抽象工厂模式
{
    public class WinFactory : GUIFactory
    {
        public IButton CreateButton()
        {
            return new WinButton();   
        }

        public ICheckBox CreateCheckBox()
        {
            return new WinCheckBox();
        }
    }
}
