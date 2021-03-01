using System;
using System.Collections.Generic;
using System.Text;

namespace CreationDesign.建造者模式
{
    public class TeslaBuilder : IBuilder
    {
        private Car _car = new Car();
        public void BuilderChassis()
        {
            _car.Add("特斯拉底盘");
        }

        public void BuilderEngine()
        {
            _car.Add("电池");
        }

        public void BuilderTires()
        {
            _car.Add("轮胎");
        }

        public Car Builder()
        {
            return _car;
        }
    }
}
