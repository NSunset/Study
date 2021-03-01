using System;
using System.Collections.Generic;
using System.Text;

namespace CreationDesign.抽象工厂模式
{
    public class MacCheckBox : ICheckBox
    {
        public void Paint()
        {
            Console.WriteLine("获取MacOs复选框");
        }
    }
}
