using System;
using System.Collections.Generic;
using System.Text;

namespace CreationDesign.建造者模式
{
    public class Director
    {
        private IBuilder _bulder;
        public IBuilder Builder
        {
            set
            {
                _bulder = value;
            }
        }

        public void ComponentTesla()
        {
            _bulder.BuilderChassis();

            _bulder.BuilderEngine();

            _bulder.BuilderTires();
        }

        public void ComponentBiYaDi()
        {
            _bulder.BuilderChassis();
            _bulder.BuilderEngine();
            _bulder.BuilderTires();
        }
    }
}
