namespace ExpressionsTests
{
    public interface ICustomContainer
    {
        T Resolve<T>() where T : class;
    }
}
