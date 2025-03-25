using DbRepos;
using Microsoft.Extensions.Logging;
using Models;
using Models.DTO;
 
namespace Services;
 
 
public class BrandServiceDb : IBrandService {
 
    private readonly BrandDbRepos _brandRepo;
    private readonly ILogger<BrandServiceDb> _logger;    
   
    public BrandServiceDb(BrandDbRepos brandRepo, ILogger<BrandServiceDb> logger)
    {
        _brandRepo = brandRepo;
        _logger = logger;
    }
 
    public Task<ResponseItemDto<GstUsrInfoAllDto>> InfoAsync() => _brandRepo.InfoAsync();
    public Task<ResponseItemDto<GstUsrInfoAllDto>> SeedAsync(int nrOfItems) => _brandRepo.SeedAsync(nrOfItems);
    public Task<ResponseItemDto<GstUsrInfoAllDto>> RemoveSeedAsync(bool seeded) => _brandRepo.RemoveSeedAsync(seeded);
    public Task<UsrInfoDto> SeedUsersAsync(int nrOfUsers, int nrOfSuperUsers, int nrOfSysBrand) => _brandRepo.SeedUsersAsync(nrOfUsers, nrOfSuperUsers, nrOfSysBrand);
 
}

