using Dapper;
using RealEstate_Dapper_Api.Context;
using RealEstate_Dapper_Api.DTOS.AdressDtos;

namespace RealEstate_Dapper_Api.Repository.Adress
{
    public class AdressRepository : IAdressRepository
    {
        private readonly DapperContext _context;

        public AdressRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(CreateAdressDto dto)
        {
            string query = "Insert Into Adress (AdressTitle1, Description, AdressTitle2, Phone1, Phone2, Email, Location) Values (@AdressTitle1, @Description, @AdressTitle2, @Phone1, @Phone2, @Email, @Location)";
            var parameters = new DynamicParameters();
            parameters.Add("@AdressTitle1", dto.AdressTitle1);
            parameters.Add("@Description", dto.Description);
            parameters.Add("@AdressTitle2", dto.AdressTitle2);
            parameters.Add("@Phone1", dto.Phone1);
            parameters.Add("@Phone2", dto.Phone2);
            parameters.Add("@Email", dto.Email);
            parameters.Add("@Location", dto.Location);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteAsync(int id)
        {
            string query = "Delete From Adress Where AdressId = @id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultAdressDto>> GetAllAsync()
        {
            string query = "Select * From Adress";

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultAdressDto>(query);
                return values.ToList();
            }
        }

        public async Task<GetAdressByIdDto> GetByIdAsync(int id)
        {
            string query = "Select * From Adress Where AdressId = @id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<GetAdressByIdDto>(query, parameters);
            }
        }

        public async Task UpdateAsync(UpdateAdressDto dto)
        {
            string query = "Update Adress Set AdressTitle1 = @AdressTitle1, Description = @Description, AdressTitle2 = @AdressTitle2, Phone1 = @Phone1, Phone2 = @Phone2, Email = @Email, Location = @Location Where AdressId = @id";
            var parameters = new DynamicParameters();
            parameters.Add("@AdressTitle1", dto.AdressTitle1);
            parameters.Add("@Description", dto.Description);
            parameters.Add("@AdressTitle2", dto.AdressTitle2);
            parameters.Add("@Phone1", dto.Phone1);
            parameters.Add("@Phone2", dto.Phone2);
            parameters.Add("@Email", dto.Email);
            parameters.Add("@Location", dto.Location);
            parameters.Add("@id", dto.AdressId);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}