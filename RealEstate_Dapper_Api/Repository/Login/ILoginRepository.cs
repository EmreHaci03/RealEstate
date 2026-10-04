using RealEstate_Dapper_Api.DTOS.LoginDtos;
using RealEstate_Dapper_Api.Tools;

namespace RealEstate_Dapper_Api.Repository.Login
{
    public interface ILoginRepository
    {
        Task<GetCheckAppUserViewModel> CheckUserAsync(ResultLoginDto dto);

    }
}
