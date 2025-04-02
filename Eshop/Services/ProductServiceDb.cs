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
    private readonly SizeDbrepos _sizeRepo;
    private readonly UserDbRepos _userRepo;
    private readonly ILogger<ProductServiceDb> _logger;

    public ProductServiceDb(
        ProductDbRepos productRepo, OrderDbRepos orderRepo, ColorDbRepos colorRepo,
        BrandDbRepos brandRepo, SizeDbrepos sizeRepo, UserDbRepos userRepo,
        ILogger<ProductServiceDb> logger)
    {
        _productRepo = productRepo;
        _orderRepo = orderRepo;
        _colorRepo = colorRepo;
        _brandRepo = brandRepo;
        _sizeRepo = sizeRepo;
        _userRepo = userRepo;
        _logger = logger;
    }
 

        public Task<ResponsePageDTO<IProduct>> ReadProductsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize) => _productRepo.ReadItemsAsync(seeded, flat, filter, pageNumber, pageSize);
        public Task<ResponseItemDTO<IProduct>> ReadProductAsync(Guid id, bool flat) => _productRepo.ReadItemAsync(id, flat);
        public Task<ResponseItemDTO<IProduct>> DeleteProductAsync(Guid id) => _productRepo.DeleteItemAsync(id);
        public Task<ResponseItemDTO<IProduct>> UpdateProductAsync(ProductDTO item) => _productRepo.UpdateItemAsync(item);
        public Task<ResponseItemDTO<IProduct>> CreateProductAsync(ProductDTO item) => _productRepo.CreateItemAsync(item);

        public Task<ResponsePageDTO<IOrder>> ReadOrdersAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize) => _orderRepo.ReadItemsAsync(seeded, flat, filter, pageNumber, pageSize);
        public Task<ResponseItemDTO<IOrder>> ReadOrderAsync(Guid id, bool flat) => _orderRepo.ReadItemAsync(id, flat);
        public Task<ResponseItemDTO<IOrder>> DeleteOrderAsync(Guid id) => _orderRepo.DeleteItemAsync(id);
        public Task<ResponseItemDTO<IOrder>> UpdateOrderAsync(OrderDTO item) => _orderRepo.UpdateItemAsync(item);
        public Task<ResponseItemDTO<IOrder>> CreateOrderAsync(OrderDTO item) => _orderRepo.CreateItemAsync(item);

        public Task<ResponsePageDTO<IColor>> ReadColorsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize) => _colorRepo.ReadItemsAsync(seeded, flat, filter, pageNumber, pageSize);
        public Task<ResponseItemDTO<IColor>> ReadColorAsync(Guid id, bool flat) => _colorRepo.ReadItemAsync(id, flat);
        public Task<ResponseItemDTO<IColor>> DeleteColorAsync(Guid id) => _colorRepo.DeleteItemAsync(id);
        public Task<ResponseItemDTO<IColor>> UpdateColorAsync(ColorDTO item) => _colorRepo.UpdateItemAsync(item);
        public Task<ResponseItemDTO<IColor>> CreateColorAsync(ColorDTO item) => _colorRepo.CreateItemAsync(item);

        public Task<ResponsePageDTO<IBrand>> ReadShoeBrandsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize) => _brandRepo.ReadItemsAsync(seeded, flat, filter, pageNumber, pageSize);
        public Task<ResponseItemDTO<IBrand>> ReadShoeBrandAsync(Guid id, bool flat) => _brandRepo.ReadItemAsync(id, flat);
        public Task<ResponseItemDTO<IBrand>> DeleteShoeBrandAsync(Guid id) => _brandRepo.DeleteItemAsync(id);
        public Task<ResponseItemDTO<IBrand>> UpdateShoeBrandAsync(BrandDTO item) => _brandRepo.UpdateItemAsync(item);
        public Task<ResponseItemDTO<IBrand>> CreateShoeBrandAsync(BrandDTO item) => _brandRepo.CreateItemAsync(item);

        public Task<ResponsePageDTO<ISize>> ReadShoeSizesAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize) => _sizeRepo.ReadItemsAsync(seeded, flat, filter, pageNumber, pageSize);
        public Task<ResponseItemDTO<ISize>> ReadShoeSizeAsync(Guid id, bool flat) => _sizeRepo.ReadItemAsync(id, flat);
        public Task<ResponseItemDTO<ISize>> DeleteShoeSizeAsync(Guid id) => _sizeRepo.DeleteItemAsync(id);
        public Task<ResponseItemDTO<ISize>> UpdateShoeSizeAsync(SizeDTO item) => _sizeRepo.UpdateItemAsync(item);
        public Task<ResponseItemDTO<ISize>> CreateShoeSizeAsync(SizeDTO item) => _sizeRepo.CreateItemAsync(item);

        public Task<ResponsePageDTO<IUser>> ReadUsersAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize) => _userRepo.ReadItemsAsync(seeded, flat, filter, pageNumber, pageSize);
        public Task<ResponseItemDTO<IUser>> ReadUserAsync(Guid id, bool flat) => _userRepo.ReadItemAsync(id, flat);
        public Task<ResponseItemDTO<IUser>> DeleteUserAsync(Guid id) => _userRepo.DeleteItemAsync(id);
        public Task<ResponseItemDTO<IUser>> UpdateUserAsync(UserDTO item) => _userRepo.UpdateItemAsync(item);
        public Task<ResponseItemDTO<IUser>> CreateUserAsync(UserDTO item) => _userRepo.CreateItemAsync(item);

    
}