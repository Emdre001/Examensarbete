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
 
    public Task<ResponseItemDTO<GstUsrInfoAllDTO>> InfoAsync() => _orderRepo.InfoAsync();
    public Task<ResponseItemDTO<GstUsrInfoAllDTO>> SeedAsync(int nrOfItems) => _orderRepo.SeedAsync(nrOfItems);
    public Task<ResponseItemDTO<GstUsrInfoAllDTO>> RemoveSeedAsync(bool seeded) => _orderRepo.RemoveSeedAsync(seeded);
    public Task<UsrInfoDTO> SeedUsersAsync(int nrOfUsers, int nrOfSuperUsers, int nrOfSysBrand) => _orderRepo.SeedUsersAsync(nrOfUsers, nrOfSuperUsers, nrOfSysBrand);
 
}

