using Models;
using Models.DTO;
 
namespace Services;
 
public interface IProductService {
 
    public Task<ResponsePageDto<IProduct>> ReadProductsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize);
    public Task<ResponseItemDto<IProduct>> ReadProductAsync(Guid id, bool flat);
    public Task<ResponseItemDto<IProduct>> DeleteProductAsync(Guid id);
    public Task<ResponseItemDto<IProduct>> UpdateProductAsync(ProductDto item);
    public Task<ResponseItemDto<IProduct>> CreateProductAsync(ProductDto item);
 
    public Task<ResponsePageDto<IOrder>> ReadOrdersAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize);
    public Task<ResponseItemDto<IOrder>> ReadOrderAsync(Guid id, bool flat);
    public Task<ResponseItemDto<IOrder>> DeleteOrderAsync(Guid id);
    public Task<ResponseItemDto<IOrder>> UpdateOrderAsync(OrderDto item);
    public Task<ResponseItemDto<IOrder>> CreateOrderAsync(OrderDto item);
 
    public Task<ResponsePageDto<IColor>> ReadColorsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize);
    public Task<ResponseItemDto<IColor>> ReadColorAsync(Guid id, bool flat);
    public Task<ResponseItemDto<IColor>> DeleteColorAsync(Guid id);
    public Task<ResponseItemDto<IColor>> UpdateColorAsync(ColorDto item);
    public Task<ResponseItemDto<IColor>> CreateColorAsync(ColorDto item);

    public Task<ResponsePageDto<IShoeBrand>> ReadShoeBrandsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize);
    public Task<ResponseItemDto<IShoeBrand>> ReadShoeBrandAsync(Guid id, bool flat);
    public Task<ResponseItemDto<IShoeBrand>> DeleteShoeBrandAsync(Guid id);
    public Task<ResponseItemDto<IShoeBrand>> UpdateShoeBrandAsync(ShoeBrandDto item);
    public Task<ResponseItemDto<IShoeBrand>> CreateShoeBrandAsync(ShoeBrandDto item);

    public Task<ResponsePageDto<IShoeSize>> ReadShoeSizesAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize);
    public Task<ResponseItemDto<IShoeSize>> ReadShoeSizeAsync(Guid id, bool flat);
    public Task<ResponseItemDto<IShoeSize>> DeleteShoeSizeAsync(Guid id);
    public Task<ResponseItemDto<IShoeSize>> UpdateShoeSizeAsync(ShoeSizeDto item);
    public Task<ResponseItemDto<IShoeSize>> CreateShoeSizeAsync(ShoeSizeDto item);

    public Task<ResponsePageDto<IUser>> ReadUsersAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize);
    public Task<ResponseItemDto<IUser>> ReadUserAsync(Guid id, bool flat);
    public Task<ResponseItemDto<IUser>> DeleteUserAsync(Guid id);
    public Task<ResponseItemDto<IUser>> UpdateUserAsync(UserDto item);
    public Task<ResponseItemDto<IUser>> CreateUserAsync(UserDto item);

}