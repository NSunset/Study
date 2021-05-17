using System;

namespace ExpressionsTests
{
    public class MissingObjectException : Exception
    {
        public Type ResolveType { get; set; }

        public MissingObjectException(Type resolveType) : this($"Resolve 没有找到{resolveType}", resolveType)
        {
            ResolveType = resolveType;
        }

        public MissingObjectException(string? message, Type resolveType) : base(message)
        {
            ResolveType = resolveType;
        }

        public MissingObjectException(string? message, Exception? innerException, Type resolveType) : base(message, innerException)
        {
            ResolveType = resolveType;
        }
    }
}
