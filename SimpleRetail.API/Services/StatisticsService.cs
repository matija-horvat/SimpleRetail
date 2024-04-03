using SimpleRetail.API.Contracts;
using SimpleRetail.Common.Responses;
using SimpleRetail.Data.Contracts;

namespace SimpleRetail.API.Services;

public class StatisticsService : IStatisticsService
{
    private readonly IStatisticsRepository _statisticsRepository;

    public StatisticsService(IStatisticsRepository statisticsRepository)
    {
        _statisticsRepository = statisticsRepository;
    }

    public async Task<BestOfferProductResponse> GetBestOfferForProduct(Guid productId)
    {
        return await _statisticsRepository.GetBestOfferForProduct(productId);
    }

    public async Task<IEnumerable<PurchasedItemsFromSuppliersResponse>> GetPurchasedItems(Guid supplierId)
    {
        return await _statisticsRepository.GetPurchasedItems(supplierId);
    }
}
