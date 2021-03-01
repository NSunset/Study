using System;
using System.Collections.Generic;
using System.Text;

namespace CreationDesign.抽象工厂模式
{
    public class WinButton : IButton
    {
        public void Paint()
        {
            Console.WriteLine("获取Windows按钮");
        }
    }
}
