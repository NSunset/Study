using lib.Data.Initial;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lib.EntityConfiguration.Initial
{
    public class OrderConfiguration : BaseConfiguration<Order>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.OrderPlaced).HasDefaultValue();
            builder.Property(x => x.OrderFulfilled).HasDefaultValue();
            builder.Property(x => x.CustomerId).IsRequired();

        }
    }
}
