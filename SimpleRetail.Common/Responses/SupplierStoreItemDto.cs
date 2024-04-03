namespace SimpleRetail.Common.Responses;

public class SupplierStoreItemDto
{
    public Guid SupplierId { get; set; }
    public Guid StoreId { get; set; }
    public Guid ItemId { get; set; }
    public int Priority { get; set; }
}
