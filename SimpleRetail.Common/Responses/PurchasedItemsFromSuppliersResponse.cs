namespace SimpleRetail.Common.Responses;

public class PurchasedItemsFromSuppliersResponse
{
    public Guid ItemId { get; set; }
    public string ItemCode { get; set; } = string.Empty;
    public string ItemName { get; set; } = string.Empty;
    public decimal TotalQuantity { get; set; }
    public decimal TotalPrice { get; set; }
}
