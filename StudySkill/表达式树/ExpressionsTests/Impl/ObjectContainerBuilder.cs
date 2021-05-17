using System;
using System.Collections.Generic;

namespace ExpressionsTests
{
    public class ObjectContainerBuilder : IObjectBuilder
    {
        /// <summary>
        /// key：接口。value：值
        /// </summary>
        private readonly IDictionary<Type, Type> _objectPair = new Dictionary<Type, Type>();

        public ICustomContainer Build()
        {
            var factoryHandler = new SimpleObjectFactoryHandler();

            ICustomContainer container = new ObjectCustomContainer(GetObjectHandler());
           
            IEnumerable<IObjectFactoryHandler> GetObjectHandler()
            {
                factoryHandler.ObjectPair = _objectPair;
                yield return factoryHandler;
            }

            factoryHandler.CustomContainer = container;
            return container;
        }

        public void Register<ImplType, InterfaceType>()
        {
            _objectPair[typeof(InterfaceType)] = typeof(ImplType);
        }
    }
}
