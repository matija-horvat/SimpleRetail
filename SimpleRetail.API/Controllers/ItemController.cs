using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleRetail.API.Contracts;
using SimpleRetail.API.Services;
using SimpleRetail.Common.Requests;
using SimpleRetail.Common.Responses;

namespace SimpleRetail.API.Controllers;

[Route("api/[controller]")]
public class ItemController: ControllerBase
{
    private readonly IItemService _itemService;

    public ItemController(IItemService itemService)
    {
        _itemService = itemService;
    }

    /// <summary>
    /// Get list of Items.
    /// </summary>
    /// <returns></returns>
    [HttpGet("getAll")]
    [AllowAnonymous]
    //[Authorize(AuthenticationSchemes = DestAuthSchemeConstants.SchemaName)]
    public async Task<ActionResult<IEnumerable<ItemDto>>> GetAll([FromQuery] int page, int recordsToTake)
    {
        var dtos = await _itemService.GetAll(page, recordsToTake);
        if (dtos == null || dtos.Count() == 0) { return NotFound("Cannot find data."); }
        return Ok(dtos);
    }

    /// <summary>
    /// Get single Item details.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ItemDto>> Get(Guid id)
    {
        var dto = await _itemService.GetById(id);
        if (dto is null) { return NotFound("Cannot find data."); }
        return Ok(dto);
    }


    /// <summary>
    /// Create new Item.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("create")]
    public async Task<IActionResult> Post([FromBody] ChangeItemRequest request)
    {
        var result = await _itemService.CreateAsync(request);
        return Ok(result);
    }

    /// <summary>
    /// Update existing Item.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("update")]
    public async Task<IActionResult> Put([FromBody] ChangeItemRequest request)
    {
        var result = await _itemService.UpdateAsync(request);
        return Ok(result);
    }

    /// <summary>
    /// Delete existing Item (soft).
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromQuery] Guid id, Guid changeUserId)
    {
        await _itemService.DeleteAsync(id, changeUserId);
        return Ok();
    }
}
