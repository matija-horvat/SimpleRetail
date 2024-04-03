using Microsoft.EntityFrameworkCore;
using SimpleRetail.Common.Responses;
using SimpleRetail.Data.Contracts;
using SimpleRetail.Data.EF;

namespace SimpleRetail.Data.Repositories;

public class StatisticsRepository : IStatisticsRepository
{
    protected readonly IDbContextFactory<DataContext> _contextFactory;

    public StatisticsRepository(IDbContextFactory<DataContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<IEnumerable<PurchasedItemsFromSuppliersResponse>> GetPurchasedItems(Guid supplierId)
    {
        using var newContext = await _contextFactory.CreateDbContextAsync();

        var result = await newContext.OrderDetails
                                .Where(od => od.SupplierId == supplierId)
                                .Join(newContext.Items, od => od.ItemId, i => i.Id,
                                      (od, i) => new
                                      {
                                          ItemId = i.Id,
                                          ItemCode = i.Code,
                                          ItemName = i.Name,
                                          ItemQuantity = od.ItemQuantity,
                                          ItemUnitPrice = od.ItemUnitPrice
                                      })
                                .GroupBy(item => new { item.ItemId, item.ItemCode, item.ItemName })
                                .Select(group => new PurchasedItemsFromSuppliersResponse
                                {
                                    ItemId = group.Key.ItemId,
                                    ItemCode = group.Key.ItemCode,
                                    ItemName = group.Key.ItemName,
                                    TotalQuantity = group.Sum(item => item.ItemQuantity),
                                    TotalPrice = group.Sum(item => item.ItemUnitPrice)
                                })
                                .OrderBy(item => item.ItemCode)
                                .ThenBy(item => item.ItemName)
                                .ToListAsync();

        return result;
    }

    public async Task<BestOfferProductResponse?> GetBestOfferForProduct(Guid productId)
    {
        using var newContext = await _contextFactory.CreateDbContextAsync();

        //var result = await newContext.Procurements
        //                            .Where(p => p.ProcurementDetails.Any(pd => pd.ItemId == productId))
        //                            .SelectMany(p => p.ProcurementDetails
        //                                .Where(pd => pd.ItemId == productId)
        //                                .OrderBy(pd => pd.ItemUnitPrice)
        //                                .Take(1)
        //                                .Select(pd => new BestOfferProductResponse
        //                                {
        //                                    SupplierId = p.Supplier.Id,
        //                                    SupplierName = p.Supplier.Name,
        //                                    ItemId = pd.Item.Id,
        //                                    ItemCode = pd.Item.Code,
        //                                    ItemName = pd.Item.Name,
        //                                    Price = pd.ItemUnitPrice
        //                                }))
        //                            .FirstOrDefaultAsync();

        var result = await newContext.Procurements
                                    .Join(newContext.ProcurementDetails, p => p.Id, pd => pd.ProcurementId, (p, pd) => new { Procurement = p, ProcurementDetail = pd })
                                    .Join(newContext.Suppliers,ppd => ppd.Procurement.SupplierId,s => s.Id,(ppd, s) => new { ProcurementAndDetail = ppd, Supplier = s })
                                    .Join(newContext.Items,pps => pps.ProcurementAndDetail.ProcurementDetail.ItemId,i => i.Id,
                                        (pps, i) => new { Supplier = pps.Supplier, Item = i, ProcurementDetail = pps.ProcurementAndDetail.ProcurementDetail })
                                    .Where(ppsi => ppsi.ProcurementDetail.ItemId == productId)
                                    .OrderBy(ppsi => ppsi.ProcurementDetail.ItemUnitPrice)
                                    .Select(ppsi => new BestOfferProductResponse
                                    {
                                        SupplierId = ppsi.Supplier.Id,
                                        SupplierName = ppsi.Supplier.Name,
                                        ItemId = ppsi.Item.Id,
                                        ItemCode = ppsi.Item.Code,
                                        ItemName = ppsi.Item.Name,
                                        Price = ppsi.ProcurementDetail.ItemUnitPrice
                                    })
                                    .FirstOrDefaultAsync();

        return result;
    }
}
