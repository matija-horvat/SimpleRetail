using SimpleRetail.Data.EF.Model.Common;
using System.ComponentModel.DataAnnotations;

namespace SimpleRetail.Data.EF.Model;

public class Order : IAuditAttributes
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    public string Code { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public Guid CustomerId { get; set; }
    public Guid StoreId { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }
    public decimal TotalDiscount { get; set; }
    public decimal TotalFee { get; set; }

    public string DeliveryLocation { get; set; } = string.Empty;
    public bool? isCanceled { get; set; } = false;
    public bool? isCompleted { get; set; } = false;
    public bool? isDelivered { get; set; } = false;


    //LAZY LOADING: Navigation Properties
    public Store Store { get; set; }
    public Person Customer { get; set; }

    //1-to-many relationship
    public List<OrderDetail>? Items { get; set; }

    //Audit
    public DateTime InsertDate { get; set; } = DateTime.UtcNow;
    public Guid InsertUser { get; set; }
    public DateTime? UpdateDate { get; set; }
    public Guid? UpdateUser { get; set; }
}
