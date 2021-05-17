namespace ExpressionsTests
{
    public interface IObjectFactoryHandler
    {
        bool DetermineHandler<T>() where T : class;

        T ResolveObject<T>() where T : class;
    }
}
