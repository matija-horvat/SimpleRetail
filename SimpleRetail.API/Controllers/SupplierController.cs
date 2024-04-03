using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleRetail.BL.Contracts;
using SimpleRetail.Common.Requests;
using SimpleRetail.Common.Responses;

namespace SimpleRetail.API.Controllers;

[Route("api/[controller]")]
public class SupplierController: ControllerBase
{
    private readonly ISupplierService _supplierService;

    public SupplierController(ISupplierService supplierService)
    {
        _supplierService = supplierService;
    }

    /// <summary>
    /// Get list of suppliers.
    /// </summary>
    /// <returns></returns>
    [HttpGet("getAll")]
    [AllowAnonymous]
    //[Authorize(AuthenticationSchemes = DestAuthSchemeConstants.SchemaName)]
    public async Task<ActionResult<IEnumerable<SupplierDto>>> GetAll([FromQuery] int page, int recordsToTake)
    {
        var storeDtos = await _supplierService.GetAll(page, recordsToTake);
        if (storeDtos == null || storeDtos.Count() == 0) { return NotFound("Cannot find data."); }
        return Ok(storeDtos);
    }

    /// <summary>
    /// Get single supplier details.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SupplierDto>> Get(Guid id)
    {
        var dto = await _supplierService.GetById(id);
        if (dto is null) { return NotFound("Cannot find data."); }
        return Ok(dto);
    }

    /// <summary>
    /// Get single supplier details with included sub entities.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("details/{id:guid}")]
    public async Task<ActionResult<SupplierDto>> GetDetails(Guid id)
    {
        var dto = await _supplierService.GetDetailsById(id);
        if (dto is null) { return NotFound("Cannot find data."); }
        return Ok(dto);
    }

    /// <summary>
    /// Create new supplier.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("create")]
    public async Task<IActionResult> Post([FromBody] ChangeSupplierRequest request)
    {
        var result = await _supplierService.CreateAsync(request);
        return Ok(result);
    }

    /// <summary>
    /// Update existing supplier.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("update")]
    public async Task<IActionResult> Put([FromBody] ChangeSupplierRequest request)
    {
        var result = await _supplierService.UpdateAsync(request);
        return Ok(result);
    }

    /// <summary>
    /// Delete existing supplier (soft).
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromQuery] Guid id, Guid changeUserId)
    {
        await _supplierService.DeleteAsync(id, changeUserId);
        return Ok();
    }
}
