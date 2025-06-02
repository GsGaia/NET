using System.Net;
using Gaia.Domain.Entity;
using Gaia.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class RequestionController : ControllerBase
{
    private readonly RequestionService _service;

    public RequestionController(RequestionService service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAll()
    {
        var requestions = await _service.GetAllAsync();
        return Ok(requestions);
    }

    [HttpGet("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetById(long id)
    {
        var requestion = await _service.GetByIdAsync(id);
        if (requestion == null) return NotFound("Solicitação não encontrada.");
        return Ok(requestion);
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Create([FromBody] Requestion requestion)
    {
        try
        {
            var created = await _service.CreateAsync(requestion);
            return CreatedAtAction(nameof(GetById), new { id = created.IdRequestion }, created);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Update(long id, [FromBody] Requestion requestion)
    {
        try
        {
            if (id != requestion.IdRequestion) return BadRequest();
            var updated = await _service.UpdateAsync(requestion, id);
            return Ok(updated);
        }
        catch (Exception ex)
        {
            return NotFound(new { error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Delete(long id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(new { error = ex.Message });
        }
    }
}
