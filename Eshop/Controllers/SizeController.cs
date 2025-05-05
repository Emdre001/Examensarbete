using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using DbRepos;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SizeController : ControllerBase
{
    private readonly SizeDbRepos _sizeRepo;

    public SizeController(SizeDbRepos sizeRepo)
    {
        _sizeRepo = sizeRepo;
    }

    // GET: api/size
    [HttpGet]
    public async Task<ActionResult<List<SizeDTO>>> GetAll()
    {
        var sizes = await _sizeRepo.GetAllSizesAsync();
        return Ok(sizes);
    }

    // GET: api/size/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<SizeDTO>> GetById(Guid id)
    {
        var size = await _sizeRepo.GetSizeByIdAsync(id);
        if (size == null) return NotFound();
        return Ok(size);
    }

    // POST: api/size
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] SizeDTO dto)
    {
        var success = await _sizeRepo.AddSizeAsync(dto);
        if (!success) return BadRequest("Failed to add size.");
        return CreatedAtAction(nameof(GetById), new { id = dto.SizeId }, dto);
    }

    // PUT: api/size/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] SizeDTO dto)
    {
        if (id != dto.SizeId) return BadRequest("Mismatched ID.");

        var success = await _sizeRepo.UpdateSizeAsync(dto);
        if (!success) return NotFound();

        return NoContent();
    }

    // DELETE: api/size/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _sizeRepo.DeleteSizeAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}
