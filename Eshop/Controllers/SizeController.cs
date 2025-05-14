using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;
using DbRepos;


namespace Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class SizeController : Controller
{
    private readonly ILogger<SizeController> _logger;
    private readonly SizeDbRepos _sizeRepo;

    public SizeController(ILogger<SizeController> logger, SizeDbRepos sizeRepo)
    {
        _logger = logger;
        _sizeRepo = sizeRepo;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSize([FromBody] SizeDTO sizeDto)
    {
        if (sizeDto == null)
        {
            return BadRequest("Invalid data.");
        }
        var size = await _sizeRepo.CreateSizeAsync(sizeDto);

        return CreatedAtAction(nameof(GetSizeById), new { sizeId = size.SizeId }, size);
    }

    [HttpGet("{sizeId}")]
    public async Task<IActionResult> GetSizeById(Guid sizeId)
    {
        var size = await _sizeRepo.GetSizeByIdAsync(sizeId);

        if (size == null)
        {
            return NotFound($"size with ID {sizeId} not found.");
        }

        return Ok(size);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSizes()
    {
        var sizes = await _sizeRepo.GetAllSizesAsync();
        return Ok(sizes);
    }

    [HttpPut("{sizeId}")]
    public async Task<IActionResult> UpdateSize(Guid sizeId, [FromBody] SizeDTO sizeDto)
    {
        if (sizeDto == null)
        {
            return BadRequest("Invalid data.");
        }

        var updatedSize = await _sizeRepo.UpdateSizeAsync(sizeId, sizeDto);

        if (updatedSize == null)
        {
            return NotFound($"Size with ID {sizeId} not found.");
        }

        return Ok(updatedSize);
    }

    [HttpDelete("{sizeId}")]
    public async Task<IActionResult> DeleteSize(Guid sizeId)
    {
        var success = await _sizeRepo.DeleteSizeAsync(sizeId);

        if (!success)
        {
            return NotFound($"Size with ID {sizeId} not found.");
        }

        return NoContent(); // 204 No Content
    }
}
