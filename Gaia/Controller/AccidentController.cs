using System.Net;
using Gaia.Domain.Entity;
using Gaia.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gaia.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccidentController : ControllerBase
    {
        private readonly AccidentService _service;

        public AccidentController(AccidentService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var accidents = await _service.GetAllAsync();
            return Ok(accidents);
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(long id)
        {
            var accident = await _service.GetByIdAsync(id);
            if (accident == null) return NotFound("Acidente não encontrado.");
            return Ok(accident);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] Accident accident)
        {
            try
            {
                var created = await _service.CreateAsync(accident);
                return CreatedAtAction(nameof(GetById), new { id = created.IdAccident }, created);
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
        public async Task<IActionResult> Update(long id, [FromBody] Accident accident)
        {
            try
            {
                if (id != accident.IdAccident) return BadRequest("O ID da URL não corresponde ao ID do corpo da requisição.");

                var updated = await _service.UpdateAsync(id, accident);
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
}
