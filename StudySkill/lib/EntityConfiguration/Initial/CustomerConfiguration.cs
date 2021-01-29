using lib.Data.Initial;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lib.EntityConfiguration.Initial
{
    public class CustomerConfiguration:BaseConfiguration<Customer>
    {
        public override void Configure(EntityTypeBuilder<Customer> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.City).HasColumnType("varchar(20)");
            builder.Property(x => x.Country).HasColumnType("varchar(20)");
            builder.Property(x => x.Email).HasColumnType("varchar(20)");
            builder.Property(x => x.FirstName)
                .HasColumnType("varchar(20)")
                .IsRequired();

            builder.Property(x => x.LastName)
                .HasColumnType("varchar(20)")
                .IsRequired();

            builder.Property(x => x.Phone).HasColumnType("varchar(20)");
            builder.Property(x => x.PostalCode).HasColumnType("varchar(20)");
            builder.Property(x => x.StateOrProvinceAbbr).HasColumnType("varchar(20)");
            builder.Property(x => x.StreetAddress).HasColumnType("varchar(20)");

        }
    }
}
