using Models;
using Models.DTO;
 
namespace Services;
 
public interface IProductService {
 
    public Task<ResponsePageDTO<IProduct>> ReadProductsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize);
    public Task<ResponseItemDTO<IProduct>> ReadProductAsync(Guid id, bool flat);
    public Task<ResponseItemDTO<IProduct>> DeleteProductAsync(Guid id);
    public Task<ResponseItemDTO<IProduct>> UpdateProductAsync(ProductDTO item);
    public Task<ResponseItemDTO<IProduct>> CreateProductAsync(ProductDTO item);
 
    public Task<ResponsePageDTO<IOrder>> ReadOrdersAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize);
    public Task<ResponseItemDTO<IOrder>> ReadOrderAsync(Guid id, bool flat);
    public Task<ResponseItemDTO<IOrder>> DeleteOrderAsync(Guid id);
    public Task<ResponseItemDTO<IOrder>> UpdateOrderAsync(OrderDTO item);
    public Task<ResponseItemDTO<IOrder>> CreateOrderAsync(OrderDTO item);
 
    public Task<ResponsePageDTO<IColor>> ReadColorsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize);
    public Task<ResponseItemDTO<IColor>> ReadColorAsync(Guid id, bool flat);
    public Task<ResponseItemDTO<IColor>> DeleteColorAsync(Guid id);
    public Task<ResponseItemDTO<IColor>> UpdateColorAsync(ColorDTO item);
    public Task<ResponseItemDTO<IColor>> CreateColorAsync(ColorDTO item);

    public Task<ResponsePageDTO<IBrand>> ReadShoeBrandsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize);
    public Task<ResponseItemDTO<IBrand>> ReadShoeBrandAsync(Guid id, bool flat);
    public Task<ResponseItemDTO<IBrand>> DeleteShoeBrandAsync(Guid id);
    public Task<ResponseItemDTO<IBrand>> UpdateShoeBrandAsync(BrandDTO item);
    public Task<ResponseItemDTO<IBrand>> CreateShoeBrandAsync(BrandDTO item);

    public Task<ResponsePageDTO<ISize>> ReadShoeSizesAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize);
    public Task<ResponseItemDTO<ISize>> ReadShoeSizeAsync(Guid id, bool flat);
    public Task<ResponseItemDTO<ISize>> DeleteShoeSizeAsync(Guid id);
    public Task<ResponseItemDTO<ISize>> UpdateShoeSizeAsync(SizeDTO item);
    public Task<ResponseItemDTO<ISize>> CreateShoeSizeAsync(SizeDTO item);

    public Task<ResponsePageDTO<IUser>> ReadUsersAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize);
    public Task<ResponseItemDTO<IUser>> ReadUserAsync(Guid id, bool flat);
    public Task<ResponseItemDTO<IUser>> DeleteUserAsync(Guid id);
    public Task<ResponseItemDTO<IUser>> UpdateUserAsync(UserDTO item);
    public Task<ResponseItemDTO<IUser>> CreateUserAsync(UserDTO item);

}