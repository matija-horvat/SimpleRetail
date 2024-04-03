using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleRetail.Data.EF.Model;

namespace SimpleRetail.Data.EF.Configurations
{
    internal class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            //map schema
            builder.ToTable("Order", "Sales");

            builder.Property(o => o.Id).HasColumnOrder(1);
            builder.Property(o => o.Code).HasColumnOrder(2).HasColumnType("nvarchar(100)");
            builder.Property(o => o.Created).HasColumnType("datetime");
            builder.Property(o => o.TotalPrice).HasColumnType("decimal(18, 2)");
            builder.Property(o => o.TotalDiscount).HasColumnType("decimal(18, 2)");
            builder.Property(o => o.TotalFee).HasColumnType("decimal(18, 2)");
            builder.Property(o => o.isCanceled).HasDefaultValueSql("0");
            builder.Property(o => o.isCompleted).HasDefaultValueSql("0");
            builder.Property(o => o.isDelivered).HasDefaultValueSql("0");

            builder.Property(o => o.InsertDate).HasColumnType("datetime").HasDefaultValueSql("GetDate()");
            builder.Property(o => o.UpdateDate).HasColumnType("datetime");


            //1 to many relationship
            builder.HasMany(t => t.Items).WithOne(c => c.Order).HasForeignKey(c => c.OrderId)
                .OnDelete(DeleteBehavior.Restrict); //raise exception if Order have at least one Item
        }
    }
}
