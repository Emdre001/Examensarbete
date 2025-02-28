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
    private readonly ShoeBrandDbRepos _shoeBrandRepo;
    private readonly ShoeSizeDbRepos _shoeSizeRepo;
    private readonly UserDbRepos _userRepo;
    private readonly ILogger<ProductServiceDb> _logger;

    public ProductServiceDb(
        ProductDbRepos productRepo, OrderDbRepos orderRepo, ColorDbRepos colorRepo,
        ShoeBrandDbRepos shoeBrandRepo, ShoeSizeDbRepos shoeSizeRepo, UserDbRepos userRepo,
        ILogger<ProductServiceDb> logger)
    {
        _productRepo = productRepo;
        _orderRepo = orderRepo;
        _colorRepo = colorRepo;
        _shoeBrandRepo = shoeBrandRepo;
        _shoeSizeRepo = shoeSizeRepo;
        _userRepo = userRepo;
        _logger = logger;
    }
 

        public Task<ResponsePageDto<IProduct>> ReadProductsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize) => _productRepo.ReadItemsAsync(seeded, flat, filter, pageNumber, pageSize);
        public Task<ResponseItemDto<IProduct>> ReadProductAsync(Guid id, bool flat) => _productRepo.ReadItemAsync(id, flat);
        public Task<ResponseItemDto<IProduct>> DeleteProductAsync(Guid id) => _productRepo.DeleteItemAsync(id);
        public Task<ResponseItemDto<IProduct>> UpdateProductAsync(ProductDto item) => _productRepo.UpdateItemAsync(item);
        public Task<ResponseItemDto<IProduct>> CreateProductAsync(ProductDto item) => _productRepo.CreateItemAsync(item);

        public Task<ResponsePageDto<IOrder>> ReadOrdersAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize) => _orderRepo.ReadItemsAsync(seeded, flat, filter, pageNumber, pageSize);
        public Task<ResponseItemDto<IOrder>> ReadOrderAsync(Guid id, bool flat) => _orderRepo.ReadItemAsync(id, flat);
        public Task<ResponseItemDto<IOrder>> DeleteOrderAsync(Guid id) => _orderRepo.DeleteItemAsync(id);
        public Task<ResponseItemDto<IOrder>> UpdateOrderAsync(OrderDto item) => _orderRepo.UpdateItemAsync(item);
        public Task<ResponseItemDto<IOrder>> CreateOrderAsync(OrderDto item) => _orderRepo.CreateItemAsync(item);

        public Task<ResponsePageDto<IColor>> ReadColorsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize) => _colorRepo.ReadItemsAsync(seeded, flat, filter, pageNumber, pageSize);
        public Task<ResponseItemDto<IColor>> ReadColorAsync(Guid id, bool flat) => _colorRepo.ReadItemAsync(id, flat);
        public Task<ResponseItemDto<IColor>> DeleteColorAsync(Guid id) => _colorRepo.DeleteItemAsync(id);
        public Task<ResponseItemDto<IColor>> UpdateColorAsync(ColorDto item) => _colorRepo.UpdateItemAsync(item);
        public Task<ResponseItemDto<IColor>> CreateColorAsync(ColorDto item) => _colorRepo.CreateItemAsync(item);

        public Task<ResponsePageDto<IShoeBrand>> ReadShoeBrandsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize) => _shoeBrandRepo.ReadItemsAsync(seeded, flat, filter, pageNumber, pageSize);
        public Task<ResponseItemDto<IShoeBrand>> ReadShoeBrandAsync(Guid id, bool flat) => _shoeBrandRepo.ReadItemAsync(id, flat);
        public Task<ResponseItemDto<IShoeBrand>> DeleteShoeBrandAsync(Guid id) => _shoeBrandRepo.DeleteItemAsync(id);
        public Task<ResponseItemDto<IShoeBrand>> UpdateShoeBrandAsync(ShoeBrandDto item) => _shoeBrandRepo.UpdateItemAsync(item);
        public Task<ResponseItemDto<IShoeBrand>> CreateShoeBrandAsync(ShoeBrandDto item) => _shoeBrandRepo.CreateItemAsync(item);

        public Task<ResponsePageDto<IShoeSize>> ReadShoeSizesAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize) => _shoeSizeRepo.ReadItemsAsync(seeded, flat, filter, pageNumber, pageSize);
        public Task<ResponseItemDto<IShoeSize>> ReadShoeSizeAsync(Guid id, bool flat) => _shoeSizeRepo.ReadItemAsync(id, flat);
        public Task<ResponseItemDto<IShoeSize>> DeleteShoeSizeAsync(Guid id) => _shoeSizeRepo.DeleteItemAsync(id);
        public Task<ResponseItemDto<IShoeSize>> UpdateShoeSizeAsync(ShoeSizeDto item) => _shoeSizeRepo.UpdateItemAsync(item);
        public Task<ResponseItemDto<IShoeSize>> CreateShoeSizeAsync(ShoeSizeDto item) => _shoeSizeRepo.CreateItemAsync(item);

        public Task<ResponsePageDto<IUser>> ReadUsersAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize) => _userRepo.ReadItemsAsync(seeded, flat, filter, pageNumber, pageSize);
        public Task<ResponseItemDto<IUser>> ReadUserAsync(Guid id, bool flat) => _userRepo.ReadItemAsync(id, flat);
        public Task<ResponseItemDto<IUser>> DeleteUserAsync(Guid id) => _userRepo.DeleteItemAsync(id);
        public Task<ResponseItemDto<IUser>> UpdateUserAsync(UserDto item) => _userRepo.UpdateItemAsync(item);
        public Task<ResponseItemDto<IUser>> CreateUserAsync(UserDto item) => _userRepo.CreateItemAsync(item);

    
}