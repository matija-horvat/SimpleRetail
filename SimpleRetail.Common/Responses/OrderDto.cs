namespace SimpleRetail.Common.Responses;

public class OrderDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Code { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }
    public decimal TotalDiscount { get; set; }
    public decimal TotalFee { get; set; }
    public string DeliveryLocation { get; set; } = string.Empty;
    public bool? isCanceled { get; set; } = false;
    public bool? isCompleted { get; set; } = false;
    public bool? isDelivered { get; set; } = false;
    public StoreDto Store { get; set; }
    public PersonDto Customer { get; set; }
    public List<OrderDetailDto>? Items { get; set; }
}
