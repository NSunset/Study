using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ExpressionsTests
{
    public class SimpleObjectFactoryHandler : IObjectFactoryHandler
    {
        public ICustomContainer CustomContainer { get; set; }

        private IDictionary<Type, Type> _objectPair;
        /// <summary>
        /// key：接口。value：值
        /// </summary>
        public IDictionary<Type, Type> ObjectPair
        {
            get { return _objectPair; }
            set
            {
                _objectPair = value;

                foreach (var (k, v) in _objectPair)
                {
                    _objectFunc[k] = CreateObjectFunc(v);
                }
            }
        }

        private readonly IDictionary<Type, Func<object>> _objectFunc = new Dictionary<Type, Func<object>>();

        public virtual bool DetermineHandler<T>() where T : class
        {
            return ObjectPair.ContainsKey(typeof(T));
        }

        public virtual T ResolveObject<T>() where T : class
        {
            var func = _objectFunc[typeof(T)];
            T result = func.Invoke() as T;
            return result;
        }

        public Func<object> CreateObjectFunc(Type impleType)
        {
            ConstantExpression containerExp = Expression.Constant(CustomContainer, typeof(ICustomContainer));

            NewExpression studentDalExp;
            ConstructorInfo constructor = impleType.GetConstructors().FirstOrDefault();
            if (constructor == null)
            {
                studentDalExp = Expression.New(impleType);
            }
            else
            {
                ParameterInfo[] parameterInfos = constructor.GetParameters();

                Func<Type, MethodInfo> GetResolveMethod = x => typeof(ICustomContainer).GetMethod(nameof(ICustomContainer.Resolve)).MakeGenericMethod(x);

                Func<Type, MethodCallExpression> GetCallExp = x => Expression.Call(
                 containerExp,
                 GetResolveMethod(x),
                 null);

                IEnumerable<Expression> GetNewParameter()
                {
                    foreach (ParameterInfo item in parameterInfos)
                    {
                        MethodCallExpression callExp = GetCallExp(item.ParameterType);
                        yield return callExp;
                    }
                }
                studentDalExp = Expression.New(constructor, GetNewParameter());
            }

            Expression<Func<object>> lambdaExp = Expression.Lambda<Func<object>>(studentDalExp);
            Func<object> func = lambdaExp.Compile();
            return func;
        }
    }
}
