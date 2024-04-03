using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleRetail.Data.EF.Model;

namespace SimpleRetail.Data.EF.Configurations
{
    internal class ProcurementConfig : IEntityTypeConfiguration<Procurement>
    {
        public void Configure(EntityTypeBuilder<Procurement> builder)
        {
            //map schema
            builder.ToTable("Procurement", "Supply");

            builder.Property(o => o.Id).HasColumnOrder(1);
            builder.Property(o => o.Code).HasColumnOrder(2).HasColumnType("nvarchar(100)");
            builder.Property(o => o.InsertDate).HasColumnType("datetime").HasDefaultValueSql("GetDate()");
            builder.Property(o => o.UpdateDate).HasColumnType("datetime");

            //1 to many relationship
            builder.HasMany(t => t.ProcurementDetails).WithOne(c => c.Procurement).HasForeignKey(c => c.ProcurementId)
                .OnDelete(DeleteBehavior.Restrict); //raise exception if Procurement have at least one Item


        }
    }
}
