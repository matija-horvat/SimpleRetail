using SimpleRetail.Data.EF.Model.Common;

namespace SimpleRetail.Data.EF.Model;

public class Item : BaseAttributes, IAuditAttributes
{
    public string? Description { get; set; }
    public string? Manufacturer { get; set; }
    public bool? Active { get; set; }


    //1-to-many relationship
    public List<OrderDetail>? Orders { get; set; }
    public List<ProcurementDetail>? Procurements { get; set; }
    public List<SupplierItem>? SupplierItems { get; set; }


    //Audit
    public DateTime InsertDate { get; set; } = DateTime.UtcNow;
    public Guid InsertUser { get; set; }
    public DateTime? UpdateDate { get; set; }
    public Guid? UpdateUser { get; set; }
}
