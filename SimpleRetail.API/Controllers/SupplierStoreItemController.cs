using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleRetail.API.Contracts;
using SimpleRetail.Common.Requests;
using SimpleRetail.Common.Responses;

namespace SimpleRetail.API.Controllers;

[Route("api/[controller]")]
public class SupplierStoreItemController: ControllerBase
{
    private readonly ISupplierStoreItemService _supplierStoreItemService;

    public SupplierStoreItemController(ISupplierStoreItemService supplierStoreItemService)
    {
        _supplierStoreItemService = supplierStoreItemService;
    }

    /// <summary>
    /// Get list of supplier store items.
    /// </summary>
    /// <returns></returns>
    [HttpGet("getAll")]
    [AllowAnonymous]
    //[Authorize(AuthenticationSchemes = DestAuthSchemeConstants.SchemaName)]
    public async Task<ActionResult<IEnumerable<SupplierStoreItemDto>>> GetAll([FromQuery] int page, int recordsToTake)
    {
        var storeDtos = await _supplierStoreItemService.GetAll(page, recordsToTake);
        if (storeDtos == null || storeDtos.Count() == 0) { return NotFound("Cannot find data."); }
        return Ok(storeDtos);
    }

    /// <summary>
    /// Create new supplier store item link.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("create")]
    public async Task<IActionResult> Post([FromBody] SupplierStoreItemDto request)
    {
        var result = await _supplierStoreItemService.CreateAsync(request);
        return Ok(result);
    }

    /// <summary>
    /// Delete existing supplier store item link (hard).
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromBody] SupplierStoreItemDeleteRequest request)
    {
        await _supplierStoreItemService.DeleteAsync(request);
        return Ok();
    }
}
