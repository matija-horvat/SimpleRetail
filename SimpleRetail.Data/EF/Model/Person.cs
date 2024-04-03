using SimpleRetail.Data.EF.Model.Common;

namespace SimpleRetail.Data.EF.Model;

public class Person : BaseAttributes, IAuditAttributes
{
    public string? Phone { get; set; }
    public string Email { get; set; } = string.Empty;
    public bool? Active { get; set; }

    //1-to-many relationship
    public List<Supplier>? Suppliers { get; set; }
    public List<Store>? Stores { get; set; }
    public List<Order>? Orders { get; set; }
    public List<Procurement>? Procurements { get; set; }

    //Audit
    public DateTime InsertDate { get; set; } = DateTime.UtcNow;
    public Guid InsertUser { get; set; }
    public DateTime? UpdateDate { get; set; }
    public Guid? UpdateUser { get; set; }
}
