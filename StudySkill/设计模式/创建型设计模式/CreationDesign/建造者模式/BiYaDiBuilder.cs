using System;
using System.Collections.Generic;
using System.Text;

namespace CreationDesign.建造者模式
{
    public class BiYaDiBuilder : IBuilder
    {
        private Car _car = new Car();
        public void BuilderChassis()
        {
            _car.Add("比亚迪底盘");
        }

        public void BuilderEngine()
        {
            _car.Add("柴油发动机");
        }

        public void BuilderTires()
        {
            _car.Add("一汽大众");
        }

        public Car Builder()
        {
            return _car;
        }
    }
}
