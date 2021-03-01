using System;
using System.Collections.Generic;
using System.Text;

namespace CreationDesign.抽象工厂模式
{
    public class MacFactory : GUIFactory
    {
        public IButton CreateButton()
        {
            return new MacButton();
        }

        public ICheckBox CreateCheckBox()
        {
            return new MacCheckBox();
        }
    }
}
