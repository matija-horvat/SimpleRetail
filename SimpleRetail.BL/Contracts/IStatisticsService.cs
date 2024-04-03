using SimpleRetail.Common.Responses;

namespace SimpleRetail.BL.Contracts;

public interface IStatisticsService
{
    /// <summary>
    /// Retrieve statistics for a specific supplier of how many items were sold.
    /// </summary>
    /// <param name="supplierId"></param>
    /// <returns></returns>
    Task<IEnumerable<PurchasedItemsFromSuppliersResponse>> GetPurchasedItems(Guid supplierId);

    /// <summary>
    /// Retreive info about supplier who have best price offer for specific item.
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    Task<BestOfferProductResponse> GetBestOfferForProduct(Guid productId);
}
