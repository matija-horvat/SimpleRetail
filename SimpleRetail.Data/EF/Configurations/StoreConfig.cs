using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleRetail.Data.EF.Model;

namespace SimpleRetail.Data.EF.Configurations
{
    internal class StoreConfig : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            //map schema
            builder.ToTable("Store", "Store");

            builder.Property(o => o.Id).HasColumnOrder(1);
            builder.Property(o => o.Code).HasColumnOrder(2).HasColumnType("nvarchar(100)");
            builder.Property(o => o.Name).HasColumnOrder(3).HasColumnType("nvarchar(200)");
            builder.Property(o => o.Active).HasDefaultValueSql("0");
            builder.Property(o => o.InsertDate).HasColumnType("datetime").HasDefaultValueSql("GetDate()");
            builder.Property(o => o.UpdateDate).HasColumnType("datetime");


            //Order
            //1 to many relationship
            builder.HasMany(t => t.Orders).WithOne(c => c.Store).HasForeignKey(c => c.StoreId)
                .OnDelete(DeleteBehavior.Restrict); //raise exception if Store is at least on one Order

            //Procurement
            //1 to many relationship
            builder.HasMany(t => t.Procurements).WithOne(c => c.Store).HasForeignKey(c => c.StoreId)
                .OnDelete(DeleteBehavior.Restrict); //raise exception if Store have at least one Procurement


            //dijete
            //SupplierItem
        }
    }
}
