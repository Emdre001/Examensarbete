using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;
using DbRepos;


namespace Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ProductController : Controller
{
    private readonly ILogger<ProductController> _logger = null;
    private readonly ProductDbRepos _productRepo;

    public ProductController(ILogger<ProductController> logger, ProductDbRepos productRepo)
    {
        _logger = logger;
        _productRepo = productRepo;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductDTO dto)
    {
        var product = await _productRepo.CreateProductAsync(dto);
        return CreatedAtAction(nameof(Get), new { id = product.ProductId }, product);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productRepo.GetAllProductsAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var product = await _productRepo.GetProductByIdAsync(id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] ProductDTO dto)
    {
        var product = await _productRepo.UpdateProductAsync(id, dto);
        if (product == null) return NotFound();
        return Ok(product);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _productRepo.DeleteProductAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpDelete]
    [Route("DeleteAll")]
    public async Task<IActionResult> DeleteAll()
    {
        await _productRepo.DeleteAllProductsAsync();
        return Ok("All products deleted.");
    }

    
}   
