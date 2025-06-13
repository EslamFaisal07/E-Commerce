using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Data.Configrations
{
    internal class DelieveryMethodConfigurations : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.ToTable("DelievryMethod");
            builder.Property(d => d.Price).HasColumnType("decimal(8,2)");
            builder.Property(d => d.ShortName).HasColumnType("varchar").HasMaxLength(100);
            builder.Property(d => d.Description).HasColumnType("varchar").HasMaxLength(100);
            builder.Property(d => d.DeliveryTime).HasColumnType("varchar").HasMaxLength(50);



        }
    }
}
