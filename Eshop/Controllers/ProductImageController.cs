using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTO;
using DbContext;

namespace Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ProductImageController : ControllerBase
{
    private readonly MainDbContext _context;

    public ProductImageController(MainDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductImageDTO>>> GetAll()
    {
        var images = await _context.ProductImages
            .Select(img => new ProductImageDTO
            {
                ProductId = img.ProductId,
                ImageUrl = img.ImageUrl
            })
            .ToListAsync();

        return Ok(images);
    }

    [HttpGet("{productId}")]
    public async Task<ActionResult<IEnumerable<ProductImageDTO>>> GetImagesByProductId(Guid productId)
    {
        var images = await _context.ProductImages
            .Where(img => img.ProductId == productId)
            .Select(img => new ProductImageDTO
            {
                ProductId = img.ProductId,
                ImageUrl = img.ImageUrl
            })
            .ToListAsync();

        if (images == null || images.Count == 0)
            return NotFound();

        return Ok(images);
    }
       

}   
