using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressionsTests
{
    public class GreaterThanAttribute : Attribute
    {
        /// <summary>
        /// 指定目标是谁
        /// </summary>
        public string Target { get; set; }
    }

    
}
