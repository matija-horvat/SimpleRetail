using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleRetail.Data.EF.Model;

namespace SimpleRetail.Data.EF.Configurations;

internal class OrderDetailConfig : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        //map schema
        builder.ToTable("OrderDetail", "Sales");

        builder.Property(o => o.ItemQuantity).HasColumnType("decimal(18, 2)");
        builder.Property(o => o.ItemUnitPrice).HasColumnType("decimal(18, 2)");
        builder.Property(o => o.InsertDate).HasColumnType("datetime").HasDefaultValueSql("GetDate()");
        builder.Property(o => o.UpdateDate).HasColumnType("datetime");
    }
}
