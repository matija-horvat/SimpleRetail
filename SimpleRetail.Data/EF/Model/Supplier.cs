using SimpleRetail.Data.EF.Model.Common;

namespace SimpleRetail.Data.EF.Model;

public class Supplier : BaseAttributes, IAuditAttributes
{
    public string LocationHQ { get; set; } = string.Empty;
    public bool Active { get; set; }


    //foreign keys are handled in config file
    public Guid ContactId { get; set; }


    //LAZY LOADING: Navigation Properties
    public Person Contact { get; set; }

    //1-to-many relationship
    public List<OrderDetail>? Orders { get; set; }
    public List<Procurement>? Procurements { get; set; }
    public List<SupplierItem>? SupplierItems { get; set; }


    //Audit
    public DateTime InsertDate { get; set; } = DateTime.UtcNow;
    public Guid InsertUser { get; set; }
    public DateTime? UpdateDate { get; set; }
    public Guid? UpdateUser { get; set; }
}
