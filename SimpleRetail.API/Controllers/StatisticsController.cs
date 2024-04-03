using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleRetail.API.Contracts;
using SimpleRetail.Common.Responses;

namespace SimpleRetail.API.Controllers;

[Route("api/[controller]")]
public class StatisticsController: ControllerBase
{
    private readonly IStatisticsService _statisticsService;

    public StatisticsController(IStatisticsService statisticsService)
    {
        _statisticsService = statisticsService;
    }

    /// <summary>
    /// Retrieve statistics for a specific supplier of how many items were sold.
    /// </summary>
    /// <returns></returns>
    [HttpGet("supplier/{id:guid}")]
    [AllowAnonymous]
    //[Authorize(AuthenticationSchemes = DestAuthSchemeConstants.SchemaName)]
    public async Task<ActionResult<IEnumerable<PurchasedItemsFromSuppliersResponse>>> GetPurchasedItems(Guid id)
    {
        var response = await _statisticsService.GetPurchasedItems(id);
        if (response == null || response.Count() == 0) { return NotFound("Cannot find data."); }
        return Ok(response);
    }

    /// <summary>
    /// Retrieve the best offer for a specific product.
    /// </summary>
    /// <returns></returns>
    [HttpGet("best-offer/{itemId:guid}")]
    [AllowAnonymous]
    //[Authorize(AuthenticationSchemes = DestAuthSchemeConstants.SchemaName)]
    public async Task<ActionResult<PurchasedItemsFromSuppliersResponse>> GetBestOfferForProduct(Guid itemId)
    {
        var response = await _statisticsService.GetBestOfferForProduct(itemId);
        if (response == null) { return NotFound("Cannot find data."); }
        return Ok(response);
    }
}
