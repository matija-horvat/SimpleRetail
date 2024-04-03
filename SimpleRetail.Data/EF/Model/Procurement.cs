using SimpleRetail.Data.EF.Model.Common;
using System.ComponentModel.DataAnnotations;

namespace SimpleRetail.Data.EF.Model;

public class Procurement : IAuditAttributes
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    public string Code { get; set; } = string.Empty;
    public DateTime DeliveryDate { get; set; }

    //foreign keys are handled in config file
    public Guid SupplierId { get; set; }
    public Guid StoreId { get; set; }
    public Guid ReceiverId { get; set; }


    //LAZY LOADING: Navigation Properties
    public Store Store { get; set; }
    public Person Receiver { get; set; }
    public Supplier Supplier { get; set; }


    //1-to-many relationship
    public List<ProcurementDetail>? ProcurementDetails { get; set; }

    //Audit
    public DateTime InsertDate { get; set; } = DateTime.UtcNow;
    public Guid InsertUser { get; set; }
    public DateTime? UpdateDate { get; set; }
    public Guid? UpdateUser { get; set; }
}
