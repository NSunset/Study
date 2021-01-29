using lib.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lib.EntityConfiguration
{
    public class BaseConfiguration<T> : IEntityTypeConfiguration<T> where T : GeneralEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.ToTable(typeof(T).Name);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreateTime).IsRequired();
        }
    }
}
