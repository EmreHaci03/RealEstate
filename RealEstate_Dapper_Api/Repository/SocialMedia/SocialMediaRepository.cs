using Dapper;
using RealEstate_Dapper_Api.Context;
using RealEstate_Dapper_Api.DTOS.SocialMediaDtos;

namespace RealEstate_Dapper_Api.Repository.SocialMedia
{
    public class SocialMediaRepository : ISocialMediaRepository
    {
        private readonly DapperContext _context;

        public SocialMediaRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(CreateSocialMediaDto dto)
        {
            string query = "Insert Into SocialMedia (Name, Icon, SocialMediaUrl) Values (@Name, @Icon, @SocialMediaUrl)";
            var parameters = new DynamicParameters();
            parameters.Add("@Name", dto.Name);
            parameters.Add("@Icon", dto.Icon);
            parameters.Add("@SocialMediaUrl", dto.SocialMediaUrl);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteAsync(int id)
        {
            string query = "Delete From SocialMedia Where SocialMediaId = @id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultSocialMediaDto>> GetAllAsync()
        {
            string query = "Select * From SocialMedia";

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultSocialMediaDto>(query);
                return values.ToList();
            }
        }

        public async Task<GetSocialMediaByIdDto> GetByIdAsync(int id)
        {
            string query = "Select * From SocialMedia Where SocialMediaId = @id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<GetSocialMediaByIdDto>(query, parameters);
            }
        }

        public async Task UpdateAsync(UpdateSocialMediaDto dto)
        {
            string query = "Update SocialMedia Set Name = @Name, Icon = @Icon, SocialMediaUrl = @SocialMediaUrl Where SocialMediaId = @id";
            var parameters = new DynamicParameters();
            parameters.Add("@Name", dto.Name);
            parameters.Add("@Icon", dto.Icon);
            parameters.Add("@SocialMediaUrl", dto.SocialMediaUrl);
            parameters.Add("@id", dto.SocialMediaId);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}