using SimpleRetail.Data.EF.Model.Common;
using System.ComponentModel.DataAnnotations;

namespace SimpleRetail.Data.EF.Model;

public class ProcurementDetail : IItemInfoAttributes, IAuditAttributes
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    //foreign keys are handled in config file
    public Guid ProcurementId { get; set; }


    //Item info
    [Required]
    public Guid ItemId { get; set; }
    [Required]
    public decimal ItemQuantity { get; set; }
    [Required]
    public decimal ItemUnitPrice { get; set; }


    //LAZY LOADING: Navigation Properties
    public Procurement Procurement { get; set; }
    public Item Item { get; set; }


    //Audit
    public DateTime InsertDate { get; set; } = DateTime.UtcNow;
    public Guid InsertUser { get; set; }
    public DateTime? UpdateDate { get; set; }
    public Guid? UpdateUser { get; set; }
}
