using System;
using System.Collections.Generic;
using System.Text;

namespace Design.Decorator
{
    public interface IUser
    {

        [AfterNotify(2)]
        [BeforeLog(1)]
        void Login(string userName, string pwd);
    }

    public class User : IUser
    {
        public void Login(string userName, string pwd)
        {
            Console.WriteLine($"用户{userName}登录成功");
        }
    }
}
