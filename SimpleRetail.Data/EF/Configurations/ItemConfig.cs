using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleRetail.Data.EF.Model;

namespace SimpleRetail.Data.EF.Configurations
{
    internal class ItemConfig : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            //map schema
            builder.ToTable("Item", "Product");

            builder.Property(o => o.Id).HasColumnOrder(1);
            builder.Property(o => o.Code).HasColumnOrder(2).HasColumnType("nvarchar(100)");
            builder.Property(o => o.Name).HasColumnOrder(3).HasColumnType("nvarchar(200)");
            builder.Property(o => o.Active).HasDefaultValueSql("0");
            builder.Property(o => o.InsertDate).HasColumnType("datetime").HasDefaultValueSql("GetDate()");
            builder.Property(o => o.UpdateDate).HasColumnType("datetime");


            //ProcurementDetail
            //1 to many relationship
            builder.HasMany(t => t.Orders).WithOne(c => c.Item).HasForeignKey(c => c.ItemId)
                .OnDelete(DeleteBehavior.Restrict); //raise exception if Item is on at least one Order

            //OrderDetail
            //1 to many relationship
            builder.HasMany(t => t.Procurements).WithOne(c => c.Item).HasForeignKey(c => c.ItemId)
                .OnDelete(DeleteBehavior.Restrict); //raise exception if Item is on at least one Order


            //SupplierItem


        }
    }
}
