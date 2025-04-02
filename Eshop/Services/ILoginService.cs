using System;
using Models.DTO;
 
namespace Services;
 
public interface ILoginService
{
    public Task<ResponseItemDTO<LoginUserSessionDTO>> LoginUserAsync(LoginCredentialsDTO usrCreds);
}