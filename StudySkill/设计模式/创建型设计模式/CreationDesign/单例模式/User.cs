using System;
using System.Collections.Generic;
using System.Text;

namespace CreationDesign.单例模式
{
    public class User
    {

        private static User _user = null;

        private static object obj_lock = new object();

        private User() 
        {
            Console.WriteLine($"构建{nameof(User)}实例");
        }


        /// <summary>
        /// 双判断枷锁经典单例模式
        /// </summary>
        /// <returns></returns>
        public static User GetInstance()
        {
            if (_user == null)
            {
                lock (obj_lock)
                {
                    if (_user == null)
                    {
                        _user = new User();
                    }
                }
            }
            return _user;
        }
    }
}
