using System.Collections.Generic;

namespace ExpressionsTests
{
    public class ObjectCustomContainer : ICustomContainer
    {
        private readonly IEnumerable<IObjectFactoryHandler> _handlers;
        public ObjectCustomContainer(IEnumerable<IObjectFactoryHandler> handlers)
        {
            _handlers = handlers;
        }

        public T Resolve<T>() where T : class
        {
            foreach (IObjectFactoryHandler item in _handlers)
            {
                if (item.DetermineHandler<T>())
                {
                    return item.ResolveObject<T>();
                }
            }

            throw new MissingObjectException(typeof(T));
        }
    }
}
