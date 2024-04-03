namespace SimpleRetail.Data.EF.Model;

public class SupplierItem
{
    public Guid SupplierId { get; set; }
    public Guid ItemId { get; set; }
    public Guid StoreId { get; set; }
    public int Priority { get; set; }


    //LAZY LOADING: Navigation Properties
    public Supplier Supplier { get; set; }
    public Store Store { get; set; }
    public Item Item { get; set; }
}
