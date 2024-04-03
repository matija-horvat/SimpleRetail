using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleRetail.Data.EF.Model;

namespace SimpleRetail.Data.EF.Configurations
{
    internal class SupplierItemConfig : IEntityTypeConfiguration<SupplierItem>
    {
        public void Configure(EntityTypeBuilder<SupplierItem> builder)
        {
            //map schema
            builder.ToTable("SupplierItem", "Supply");

            // Configure composite primary key
            builder.HasKey(p => new { p.StoreId, p.ItemId, p.SupplierId });

            //we configure many-to-many relationship by configure 3 1-to-many relationships
            //we will configure many-to-many with intermediate entity


            //1-to-many relatioship between Store and SupplierItem
            //1 SupplierItem can have only one Store, but 1 Store can have many SupplierItem
            builder.HasOne(p => p.Store).WithMany(p => p.SupplierItems).HasForeignKey(p => p.StoreId);

            //1-to-many relatioship between Item and SupplierItem
            //1 SupplierItem can have only one Item, but 1 Item can have many SupplierItem
            builder.HasOne(p => p.Item).WithMany(p => p.SupplierItems).HasForeignKey(p => p.ItemId);

            //1-to-many relatioship between Supplier and SupplierItem
            //1 SupplierItem can have only one Supplier, but 1 Supplier can have many SupplierItems
            builder.HasOne(p => p.Supplier).WithMany(p => p.SupplierItems).HasForeignKey(p => p.SupplierId);
        }
    }
}
