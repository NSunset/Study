using System;
using System.Collections.Generic;
using System.Text;

namespace Design.Decorator
{
    /// <summary>
    /// 装饰器模式
    /// </summary>
    public abstract class UserDecorator : IUser
    {
        protected IUser _user;
        public UserDecorator(IUser user)
        {
            _user = user;
        }

        public abstract void Login(string userName, string pwd);
    }


    public class UserLogoDecorator : UserDecorator
    {
        public UserLogoDecorator(IUser user) : base(user)
        {
        }

        public override void Login(string userName, string pwd)
        {
            LoginBefore();
            _user.Login(userName, pwd);

            LoginAfter();
        }

        protected  void LoginAfter()
        {
            Console.WriteLine("登录后记录登录结果");
        }

        protected  void LoginBefore()
        {
            Console.WriteLine("登录前记录登录用户");
        }
    }

    public class UserNotifyDecorator : UserDecorator
    {
        public UserNotifyDecorator(IUser user) : base(user)
        {
        }

        public override void Login(string userName, string pwd)
        {
            _user.Login(userName, pwd);
            LoginAfter();
        }

        protected  void LoginAfter()
        {
            Console.WriteLine("在登录之后发送通知给用户");
        }
    }
}
