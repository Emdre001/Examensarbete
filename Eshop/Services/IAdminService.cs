using Models;
using Models.DTO;
 
namespace Services;
 
public interface IAdminService {
 
    public Task<ResponseItemDTO<GstUsrInfoAllDTO>> InfoAsync();
    public Task<ResponseItemDTO<GstUsrInfoAllDTO>> SeedAsync(int nrOfItems);
    public Task<ResponseItemDTO<GstUsrInfoAllDTO>> RemoveSeedAsync(bool seeded);
 
    public Task<UsrInfoDTO> SeedUsersAsync(int nrOfUsers, int nrOfSuperUsers, int nrOfSysAdmin);
 
}