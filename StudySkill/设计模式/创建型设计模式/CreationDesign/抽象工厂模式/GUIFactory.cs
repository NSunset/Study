using System;
using System.Collections.Generic;
using System.Text;

namespace CreationDesign.抽象工厂模式
{
    public interface GUIFactory
    {
        IButton CreateButton();

        ICheckBox CreateCheckBox();
    }
}
