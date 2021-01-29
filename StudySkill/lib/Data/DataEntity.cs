using System;

namespace lib.Data
{
    public abstract class DataEntity : AggregateRoot
    {
    }

    public abstract class PrimaryKeyEntity<T> : DataEntity where T : IComparable
    {
        public T Id { get; set; }
    }

    public abstract class DomainEntity : PrimaryKeyEntity<int>
    {

    }

    public abstract class GeneralEntity : DomainEntity
    {
        public DateTime CreateTime { get; set; }

    }
}
