using DbRepos;
using Microsoft.Extensions.Logging;
using Models;
using Models.DTO;
 
namespace Services;
 
 
public class OrderServiceDb : IOrderService {
 
    private readonly OrderDbRepos _orderRepo;
    private readonly ILogger<OrderServiceDb> _logger;    
   
    public OrderServiceDb(OrderDbRepos orderRepo, ILogger<OrderServiceDb> logger)
    {
        _orderRepo = orderRepo;
        _logger = logger;
    }
 
    public Task<ResponseItemDto<GstUsrInfoAllDto>> InfoAsync() => _orderRepo.InfoAsync();
    public Task<ResponseItemDto<GstUsrInfoAllDto>> SeedAsync(int nrOfItems) => _orderRepo.SeedAsync(nrOfItems);
    public Task<ResponseItemDto<GstUsrInfoAllDto>> RemoveSeedAsync(bool seeded) => _orderRepo.RemoveSeedAsync(seeded);
    public Task<UsrInfoDto> SeedUsersAsync(int nrOfUsers, int nrOfSuperUsers, int nrOfSysBrand) => _orderRepo.SeedUsersAsync(nrOfUsers, nrOfSuperUsers, nrOfSysBrand);
 
}

