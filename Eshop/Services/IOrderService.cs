using Models;
using Models.DTO;
 
namespace Services;
 
public interface IOrderService {
 
    public Task<ResponseItemDto<GstUsrInfoAllDto>> InfoAsync();
    public Task<ResponseItemDto<GstUsrInfoAllDto>> SeedAsync(int nrOfItems);
    public Task<ResponseItemDto<GstUsrInfoAllDto>> RemoveSeedAsync(bool seeded);
 
    public Task<UsrInfoDto> SeedUsersAsync(int nrOfUsers, int nrOfSuperUsers, int nrOfSysAdmin);
 
}