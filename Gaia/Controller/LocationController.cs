using System.Net;
using Gaia.Domain.Entity;
using Gaia.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gaia.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly LocationService _service;

        public LocationController(LocationService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var locations = await _service.GetAllAsync();
            return Ok(locations);
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(long id)
        {
            var location = await _service.GetByIdAsync(id);
            if (location == null) return NotFound("Localização não encontrada.");
            return Ok(location);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> Create([FromBody] Location location)
        {
            try
            {
                var created = await _service.CreateAsync(location);
                return CreatedAtAction(nameof(GetById), new { id = created.IdLocation }, created);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update(long id, [FromBody] Location location)
        {
            try
            {
                if (id != location.IdLocation) return BadRequest("O ID da URL não corresponde ao ID do corpo da requisição.");

                var updated = await _service.UpdateAsync(id, location);
                return Ok(updated);
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
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
}
