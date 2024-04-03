using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleRetail.API.Contracts;
using SimpleRetail.Common.Requests;
using SimpleRetail.Common.Responses;

namespace SimpleRetail.API.Controllers;

[Route("api/[controller]")]
public class PersonController: ControllerBase 
{
    private readonly IPersonService _personService;

    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }

    /// <summary>
    /// Get list of Persons.
    /// </summary>
    /// <returns></returns>
    [HttpGet("getAll")]
    [AllowAnonymous]
    //[Authorize(AuthenticationSchemes = DestAuthSchemeConstants.SchemaName)]
    public async Task<ActionResult<IEnumerable<PersonDto>>> GetAll([FromQuery] int page, int recordsToTake)
    {
        var dtos = await _personService.GetAll(page, recordsToTake);
        if (dtos == null || dtos.Count() == 0) { return NotFound("Cannot find data."); }
        return Ok(dtos);
    }

    /// <summary>
    /// Get single Person details.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PersonDto>> Get(Guid id)
    {
        var dto = await _personService.GetById(id);
        if (dto is null) { return NotFound("Cannot find data."); }
        return Ok(dto);
    }


    /// <summary>
    /// Create new Person.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("create")]
    public async Task<IActionResult> Post([FromBody] ChangePersonRequest request)
    {
        var result = await _personService.CreateAsync(request);
        return Ok(result);
    }

    /// <summary>
    /// Update existing Person.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("update")]
    public async Task<IActionResult> Put([FromBody] ChangePersonRequest request)
    {
        var result = await _personService.UpdateAsync(request);
        return Ok(result);
    }

    /// <summary>
    /// Delete existing Person (soft).
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromQuery] Guid id, Guid changeUserId)
    {
        await _personService.DeleteAsync(id, changeUserId);
        return Ok();
    }
}
