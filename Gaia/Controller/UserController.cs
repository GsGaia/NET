using System.Net;
using Gaia.Domain.Entity;
using Gaia.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gaia.Controller;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    
    private readonly UserService _service;

    
    public UserController(UserService service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.ServiceUnavailable)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetAll()
    {
        var users = await _service.GetAllAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.ServiceUnavailable)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetById(long id)
    {
        var user = await _service.GetByIdAsync(id);
        if (user == null) return NotFound("Usuário não encontrado.");
        return Ok(user);
    }

    
    
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.ServiceUnavailable)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Create([FromBody] User user)
    {
        try
        {
            var created = await _service.CreateAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = created.IdUser }, created);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
    
    
    

    [HttpPut("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.ServiceUnavailable)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Update(long id, [FromBody] User user)
    {
        try
        {
            var updated = await _service.UpdateAsync(id, user);
            return Ok(updated);
        }
        catch (Exception ex)
        {
            return NotFound(new { error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.ServiceUnavailable)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
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