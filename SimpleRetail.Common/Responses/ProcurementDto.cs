namespace SimpleRetail.Common.Responses;

public class ProcurementDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Code { get; set; } = string.Empty;
    public DateTime DeliveryDate { get; set; }
    public StoreDto Store { get; set; }
    public PersonDto Receiver { get; set; }
    public SupplierDto Supplier { get; set; }
    public List<ProcurementDetailDto>? Items { get; set; }
}
