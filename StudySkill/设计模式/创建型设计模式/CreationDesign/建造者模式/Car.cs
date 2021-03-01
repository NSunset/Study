using System;
using System.Collections.Generic;
using System.Text;

namespace CreationDesign.建造者模式
{
    public class Car
    {
        private IList<string> _components = new List<string>();

        public void Add(string component)
        {
            _components.Add(component);
        }

        public void Run()
        {
            var str = string.Join(",", _components);
            Console.WriteLine($"启动，点火:{str}");
        }
    }
}
