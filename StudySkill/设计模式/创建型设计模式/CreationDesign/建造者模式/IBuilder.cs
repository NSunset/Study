using System;
using System.Collections.Generic;
using System.Text;

namespace CreationDesign.建造者模式
{
    public interface IBuilder
    {
        /// <summary>
        /// 构建底盘
        /// </summary>
        void BuilderChassis();

        /// <summary>
        /// 构建发动机
        /// </summary>
        void BuilderEngine();

        /// <summary>
        /// 构建轮胎
        /// </summary>
        void BuilderTires();
    }
}
