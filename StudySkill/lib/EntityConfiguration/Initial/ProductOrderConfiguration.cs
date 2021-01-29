using lib.Data.Initial;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lib.EntityConfiguration.Initial
{
    public class ProductOrderConfiguration : BaseConfiguration<ProductOrder>
    {
        public override void Configure(EntityTypeBuilder<ProductOrder> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.OrderId).IsRequired();
            builder.Property(x => x.ProductId).IsRequired();
        }
    }
}
