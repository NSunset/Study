using System;
using System.Collections.Generic;
using System.Text;

namespace CreationDesign.单例模式
{
    public class UserTwo
    {
        public int Id { get; set; }

        private UserTwo() 
        {
            Console.WriteLine($"构建{nameof(UserTwo)}实例");
        }

        private static UserTwo _user = SetInstance();

        private static UserTwo SetInstance()
        {
            return new UserTwo();
        }

        /// <summary>
        /// 通过静态字段实例化单例模式
        /// </summary>
        /// <returns></returns>
        public static UserTwo GetInstance()
        {
            return _user;
        }
    }
}
