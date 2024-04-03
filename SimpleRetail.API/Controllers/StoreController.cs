using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleRetail.API.Contracts;
using SimpleRetail.API.Services;
using SimpleRetail.Common.Requests;
using SimpleRetail.Common.Responses;

namespace SimpleRetail.API.Controllers;

[Route("api/[controller]")]
public class StoreController: ControllerBase
{
    private readonly IStoreService _storeService;

    public StoreController(IStoreService storeService)
    {
        _storeService = storeService;
    }

    /// <summary>
    /// Get list of stores.
    /// </summary>
    /// <returns></returns>
    [HttpGet("getAll")]
    [AllowAnonymous]
    //[Authorize(AuthenticationSchemes = DestAuthSchemeConstants.SchemaName)]
    public async Task<ActionResult<IEnumerable<StoreDto>>> GetAll([FromQuery] int page, int recordsToTake)
    {
        var dtos = await _storeService.GetAll(page, recordsToTake);
        if (dtos == null || dtos.Count() == 0) { return NotFound("Cannot find data."); }
        return Ok(dtos);
    }

    /// <summary>
    /// Get single store details.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<StoreDto>> Get(Guid id)
    {
        var dto = await _storeService.GetById(id);
        if (dto is null) { return NotFound("Cannot find data."); }
        return Ok(dto);
    }

    /// <summary>
    /// Get single store details with included sub entities.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("details/{id:guid}")]
    public async Task<ActionResult<StoreDto>> GetDetails(Guid id)
    {
        var dto = await _storeService.GetDetailsById(id);
        if (dto is null) { return NotFound("Cannot find data."); }
        return Ok(dto);
    }


    /// <summary>
    /// Create new store.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("create")]
    public async Task<IActionResult> Post([FromBody] ChangeStoreRequest request)
    {
        var result = await _storeService.CreateAsync(request);
        return Ok(result);
    }

    /// <summary>
    /// Update existing store.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("update")]
    public async Task<IActionResult> Put([FromBody] ChangeStoreRequest request)
    {
        var result = await _storeService.UpdateAsync(request);
        return Ok(result);
    }

    /// <summary>
    /// Delete existing store (soft).
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromQuery] Guid id, Guid changeUserId)
    {
        await _storeService.DeleteAsync(id, changeUserId);
        return Ok();
    }
}
