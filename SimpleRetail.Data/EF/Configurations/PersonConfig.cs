using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleRetail.Data.EF.Model;

namespace SimpleRetail.Data.EF.Configurations
{
    internal class PersonConfig : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            //map schema
            builder.ToTable("Person", "People");

            builder.Property(o => o.Id).HasColumnOrder(1);
            builder.Property(o => o.Code).HasColumnOrder(2).HasColumnType("nvarchar(100)");
            builder.Property(o => o.Name).HasColumnOrder(3).HasColumnType("nvarchar(200)");
            builder.Property(o => o.InsertDate).HasColumnType("datetime").HasDefaultValueSql("GetDate()");
            builder.Property(o => o.UpdateDate).HasColumnType("datetime");

            //Supplier
            //1 to many relationship
            builder.HasMany(t => t.Suppliers).WithOne(c => c.Contact).HasForeignKey(c => c.ContactId)
                .OnDelete(DeleteBehavior.Restrict); //raise exception if Person is Contact for Supplier

            //Store
            //1 to many relationship
            builder.HasMany(t => t.Stores).WithOne(c => c.Contact).HasForeignKey(c => c.ContactId)
                .OnDelete(DeleteBehavior.Restrict); //raise exception if Person is Contact for Stores


            //Order (CustomerID)
            //1 to many relationship
            builder.HasMany(t => t.Orders).WithOne(c => c.Customer).HasForeignKey(c => c.CustomerId)
                .OnDelete(DeleteBehavior.Restrict); //raise exception if Person is Customer


            //Procurement (ReceiverID)
            //1 to many relationship
            builder.HasMany(t => t.Procurements).WithOne(c => c.Receiver).HasForeignKey(c => c.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict); //raise exception if Person is Receiver
        }
    }
}
