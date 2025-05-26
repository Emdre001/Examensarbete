using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;
using DbRepos;


namespace Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ColorController : Controller
{
    private readonly ILogger<ColorController> _logger;
    private readonly ColorDbRepos _colorRepo;

    public ColorController(ILogger<ColorController> logger, ColorDbRepos colorRepo)
    {
        _logger = logger;
        _colorRepo = colorRepo;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSize([FromBody] ColorDTO colorDto)
    {
        if (colorDto == null)
        {
            return BadRequest("Invalid data.");
        }
        var color = await _colorRepo.CreateColorAsync(colorDto);

        return CreatedAtAction(nameof(GetColorById), new { colorId = color.ColorId }, color);
    }

    [HttpGet("{colorId}")]
    public async Task<IActionResult> GetColorById(Guid colorId)
    {
        var color = await _colorRepo.GetColorByIdAsync(colorId);

        if (color == null)
        {
            return NotFound($"color with ID {colorId} not found.");
        }

        return Ok(color);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllColors()
    {
        var colors = await _colorRepo.GetAllColorsAsync();
        return Ok(colors);
    }

    [HttpPut("{colorId}")]
    public async Task<IActionResult> UpdateColor(Guid colorId, [FromBody] ColorDTO colorDto)
    {
        if (colorDto == null)
        {
            return BadRequest("Invalid data.");
        }

        var updatedColor = await _colorRepo.UpdateColorAsync(colorId, colorDto);

        if (updatedColor == null)
        {
            return NotFound($"Color with ID {colorId} not found.");
        }

        return Ok(updatedColor);
    }

    [HttpDelete("{colorId}")]
    public async Task<IActionResult> DeleteColor(Guid colorId)
    {
        var success = await _colorRepo.DeleteColorAsync(colorId);

        if (!success)
        {
            return NotFound($"Color with ID {colorId} not found.");
        }

        return NoContent(); // 204 No Content
    }
    [HttpDelete("all")]
    public async Task<IActionResult> DeleteAllColors()
    {
        await _colorRepo.DeleteAllColorsAsync();
        return NoContent();
    }
}
