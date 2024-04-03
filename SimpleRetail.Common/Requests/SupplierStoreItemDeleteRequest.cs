namespace SimpleRetail.Common.Requests;

public class SupplierStoreItemDeleteRequest
{
    public Guid SupplierId { get; set; }
    public Guid StoreId { get; set; }
    public Guid ItemId { get; set; }
}
