using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTO;
using DbContext;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductSizesController : ControllerBase
{
    private readonly MainDbContext _context;

    public ProductSizesController(MainDbContext context)
    {
        _context = context;
    }

    // GET: api/ProductSizes
    [HttpGet("/api/GetAllProductSizes")]
    public async Task<ActionResult<IEnumerable<ProductSizeDTO>>> GetAllProductSizes()
    {
        var productSizes = await _context.ProductSizes
            .Select(ps => new ProductSizeDTO
            {
                ProductId = ps.ProductId,
                SizeId = ps.SizeId,
                Stock = ps.Stock
            })
            .ToListAsync();

        return Ok(productSizes);
    }

    // GET: api/ProductSizes/{productId}/{sizeId}
    [HttpGet("/api/GetProductSizeById/{productId:guid}/{sizeId:guid}")]
    public async Task<ActionResult<ProductSizeDTO>> GetProductSizeByIds(Guid productId, Guid sizeId)
    {
        var ps = await _context.ProductSizes.FindAsync(productId, sizeId);

        if (ps == null)
            return NotFound();

        return new ProductSizeDTO
        {
            ProductId = ps.ProductId,
            SizeId = ps.SizeId,
            Stock = ps.Stock
        };
    }

    // POST: api/ProductSizes
    [HttpPost("/api/CreateProductSize")]
    public async Task<ActionResult<ProductSizeDTO>> CreateProductSize(ProductSizeDTO dto)
    {
        var exists = await _context.ProductSizes.FindAsync(dto.ProductId, dto.SizeId);
        if (exists != null)
            return Conflict("This product-size combination already exists.");

        var entity = new ProductSize
        {
            ProductId = dto.ProductId,
            SizeId = dto.SizeId,
            Stock = dto.Stock
        };

        _context.ProductSizes.Add(entity);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProductSizeByIds), new { productId = dto.ProductId, sizeId = dto.SizeId }, dto);
    }

    // PUT: api/ProductSizes/{productId}/{sizeId}
    [HttpPut("/api/updateProductSize/{productId:guid}/{sizeId:guid}")]
    public async Task<IActionResult> UpdateProductSizeByIds(Guid productId, Guid sizeId, ProductSizeDTO dto)
    {
        if (productId != dto.ProductId || sizeId != dto.SizeId)
            return BadRequest("Mismatched product/size IDs.");

        var entity = await _context.ProductSizes.FindAsync(productId, sizeId);
        if (entity == null)
            return NotFound();

        entity.Stock = dto.Stock;

        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/ProductSizes/{productId}/{sizeId}
    [HttpDelete("/api/DeleteProductSizeById/{productId:guid}/{sizeId:guid}")]
    public async Task<IActionResult> DeleteProductSizeByIds(Guid productId, Guid sizeId)
    {
        var entity = await _context.ProductSizes.FindAsync(productId, sizeId);
        if (entity == null)
            return NotFound();

        _context.ProductSizes.Remove(entity);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
