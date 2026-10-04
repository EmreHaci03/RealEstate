using Dapper;
using RealEstate_Dapper_Api.Context;
using RealEstate_Dapper_Api.DTOS.LoginDtos;
using RealEstate_Dapper_Api.Tools;

namespace RealEstate_Dapper_Api.Repository.Login
{
    public class LoginRepository : ILoginRepository
    {
        private readonly DapperContext _context;

        public LoginRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<GetCheckAppUserViewModel> CheckUserAsync(ResultLoginDto dto)
        {
            string query = @"SELECT UserId AS Id, Username AS UserName, UserRole AS Role
                 FROM AppUser
                 WHERE Username = @Username AND Password = @Password";
            var parameters = new DynamicParameters();
            parameters.Add("@Username", dto.Username);
            parameters.Add("@Password", dto.Password);

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<GetCheckAppUserViewModel>(query, parameters);

                if (values == null)
                    return new GetCheckAppUserViewModel { IsExists = false };

                values.IsExists = true;
                return values;
            }
        }
    }
}