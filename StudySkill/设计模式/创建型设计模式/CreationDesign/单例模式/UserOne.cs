using System;
using System.Collections.Generic;
using System.Text;

namespace CreationDesign.单例模式
{
    public class UserOne
    {
        private UserOne() 
        {
            Console.WriteLine($"构建{nameof(UserOne)}实例");
        }

        static UserOne()
        {
            SetInstance();
        }

        private static UserOne _user = null;

        private static void SetInstance()
        {
            _user = new UserOne();
        }

        /// <summary>
        /// 通过静态构造函数实例化单例模式
        /// </summary>
        /// <returns></returns>
        public static UserOne GetInstance()
        {
            return _user;
        }
    }
}
