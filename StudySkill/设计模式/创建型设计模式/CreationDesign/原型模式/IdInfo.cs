using System;
using System.Collections.Generic;
using System.Text;

namespace CreationDesign.原型模式
{
    public class IdInfo
    {
        public int Id { get; set; }

        /// <summary>
        /// 克隆的副本
        /// </summary>
        /// <returns></returns>
        public IdInfo GetClone()
        {
            return (IdInfo)this.MemberwiseClone();
        }
    }
}
