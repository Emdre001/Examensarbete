using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;
using Services;
using DbRepos;


namespace Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class BrandController : Controller
{
    private readonly ILogger<BrandController> _logger;
    private readonly BrandDbRepos _brandRepo;

    public BrandController(ILogger<BrandController> logger, BrandDbRepos brandRepo)
    {
        _logger = logger;
        _brandRepo = brandRepo;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBrand([FromBody] BrandDTO brandDto)
    {
        if (brandDto == null)
        {
            return BadRequest("Invalid data.");
        }
        var brand = await _brandRepo.CreateBrandAsync(brandDto);

        return CreatedAtAction(nameof(GetBrandById), new { brandId = brand.BrandId }, brand);
    }

    [HttpGet("{brandId}")]
    public async Task<IActionResult> GetBrandById(Guid brandId)
    {
        var brand = await _brandRepo.GetBrandByIdAsync(brandId);

        if (brand == null)
        {
            return NotFound($"Brand with ID {brandId} not found.");
        }

        return Ok(brand);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBrands()
    {
        var brands = await _brandRepo.GetAllBrandsAsync();
        return Ok(brands);
    }

    [HttpPut("{brandId}")]
    public async Task<IActionResult> UpdateBrand(Guid brandId, [FromBody] BrandDTO brandDto)
    {
        if (brandDto == null)
        {
            return BadRequest("Invalid data.");
        }

        var updatedBrand = await _brandRepo.UpdateBrandAsync(brandId, brandDto);

        if (updatedBrand == null)
        {
            return NotFound($"Brand with ID {brandId} not found.");
        }

        return Ok(updatedBrand);
    }

    [HttpDelete("{brandId}")]
    public async Task<IActionResult> DeleteBrand(Guid brandId)
    {
        var success = await _brandRepo.DeleteBrandAsync(brandId);

        if (!success)
        {
            return NotFound($"Brand with ID {brandId} not found.");
        }

        return NoContent(); // 204 No Content
    }
}
