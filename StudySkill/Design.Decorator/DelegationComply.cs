using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Design.Decorator
{
    public class DelegationComply : IUser
    {
        private IUser _user;
        public DelegationComply(IUser user)
        {
            _user = user;
        }

        protected Action<string, string> LoginHandler;


        public void Login(string userName, string pwd)
        {
            LoginHandler = _user.Login;

            var attributes = typeof(IUser).GetMethod("Login").GetCustomAttributes(typeof(BaseAttribute), true).OrderBy(a => ((BaseAttribute)a).Sort);
            foreach (BaseAttribute item in attributes)
            {
                LoginHandler = item.Do(LoginHandler);
            }
            LoginHandler.Invoke(userName,pwd);
        }
    }


    public abstract class BaseAttribute : Attribute
    {
        public BaseAttribute(int sort)
        {
            this.Sort = sort;
        }
        public int Sort { get; }
        public abstract Action<string,string> Do(Action<string, string> action);
    }

    /// <summary>
    /// 执行前
    /// </summary>
    public class BeforeLogAttribute : BaseAttribute
    {
        public BeforeLogAttribute(int sort) : base(sort)
        {
        }

        public override Action<string, string> Do(Action<string, string> action)
        {
            return (username, pwd) =>
            {
                Console.WriteLine("执行前执行日志记录");
                action.Invoke(username, pwd);
            };
        }
    }

    /// <summary>
    /// 执行后
    /// </summary>
    public class AfterNotifyAttribute : BaseAttribute
    {
        public AfterNotifyAttribute(int sort) : base(sort)
        {
        }

        public override Action<string, string> Do(Action<string, string> action)
        {
            return (username, pwd) =>
            {
                action.Invoke(username, pwd);
                Console.WriteLine("执行后执行消息通知");
            };
        }
    }


}
