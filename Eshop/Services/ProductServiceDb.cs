using DbRepos;
using Microsoft.Extensions.Logging;
using Models;
using Models.DTO;

namespace Services;

public class ProductServiceDb : IProductService
{
    private readonly ProductDbRepos _productRepo;
    private readonly OrderDbRepos _orderRepo;
    private readonly ColorDbRepos _colorRepo;
    private readonly BrandDbRepos _brandRepo;
    private readonly SizeDbRepos _sizeRepo;
    private readonly ILogger<ProductServiceDb> _logger;

    public ProductServiceDb(
        ProductDbRepos productRepo, OrderDbRepos orderRepo, ColorDbRepos colorRepo,
        BrandDbRepos brandRepo, SizeDbRepos sizeRepo,
        ILogger<ProductServiceDb> logger)
    {
        _productRepo = productRepo;
        _orderRepo = orderRepo;
        _colorRepo = colorRepo;
        _brandRepo = brandRepo;
        _sizeRepo = sizeRepo;
        _logger = logger;
    }
 
    
}