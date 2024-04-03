using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleRetail.Data.EF.Model;

namespace SimpleRetail.Data.EF.Configurations
{
    internal class SupplierConfig : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            //map schema
            builder.ToTable("Supplier", "Supply");

            builder.Property(o => o.Id).HasColumnOrder(1);
            builder.Property(o => o.Code).HasColumnOrder(2).HasColumnType("nvarchar(100)");
            builder.Property(o => o.Name).HasColumnOrder(3).HasColumnType("nvarchar(200)");
            builder.Property(o => o.InsertDate).HasColumnType("datetime").HasDefaultValueSql("GetDate()");
            builder.Property(o => o.UpdateDate).HasColumnType("datetime");

            //1 to many relationship
            builder.HasMany(t => t.Orders).WithOne(c => c.Supplier).HasForeignKey(c => c.SupplierId)
                .OnDelete(DeleteBehavior.Restrict); //raise exception if Supplier have at least one Item in OrderDetails

            //Procurement
            builder.HasMany(t => t.Procurements).WithOne(c => c.Supplier).HasForeignKey(c => c.SupplierId)
                .OnDelete(DeleteBehavior.Restrict); //raise exception if Supplier have at least one Procurement

            //dijete
            //SupplierItem
        }
    }
}
