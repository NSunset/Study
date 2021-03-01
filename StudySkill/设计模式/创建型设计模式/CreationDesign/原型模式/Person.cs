using System;
using System.Collections.Generic;
using System.Text;

namespace CreationDesign.原型模式
{
    public class Person
    {
        public string Name { get; set; }

        public IdInfo IdInfo { get; set; }

        public Person()
        {

        }

        /// <summary>
        /// 克隆的副本
        /// </summary>
        /// <returns></returns>
        public Person GetClone()
        {
            var person = (Person)this.MemberwiseClone();
            person.IdInfo = IdInfo.GetClone();
            return person;
        }
    }
}
