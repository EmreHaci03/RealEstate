using Dapper;
using RealEstate_Dapper_Api.Context;
using RealEstate_Dapper_Api.DTOS.FeatureCardDtos;

namespace RealEstate_Dapper_Api.Repository.FeatureCardDto
{
    public class FeatureCardRepository: IFeatureCardRepository
    {
        private readonly DapperContext _context;

        public FeatureCardRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(CreateFeatureCardDto dto)
        {
            string query = "insert into FeatureCard(Icon,SubTitle,Title,Description,ButtonText,ButtonUrl) values(@Icon,@SubTitle,@Title,@Description,@ButtonText,@ButtonUrl) ";
            var parameters = new DynamicParameters();
            parameters.Add("@Icon",dto.Icon);
            parameters.Add("@SubTitle",dto.SubTitle);
            parameters.Add("@Title",dto.Title);
            parameters.Add("@Description",dto.Description);
            parameters.Add("@ButtonText",dto.ButtonText);
            parameters.Add("@ButtonUrl",dto.ButtonUrl);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteAsync(int id)
        {
            string query = "Delete From FeatureCard Where FeatureCardId=@id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultFeatureCardDto>> GetAllAsync()
        {
            string query = "Select * From FeatureCard";
            using (var connection = _context.CreateConnection())
            {
                var values=await connection.QueryAsync<ResultFeatureCardDto>(query);
                return values.ToList();
            }
        }

        public async Task<GetFeatureCardByIdDto> GetByIdAsync(int id)
        {
            string query = "Select * From FeatureCard Where FeatureCardId=@id";
            var parameters= new DynamicParameters();
            parameters.Add("@id", id);
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<GetFeatureCardByIdDto>(query,parameters);
            }
        }

        public async Task UpdateAsync(UpdateFeatureCardDto dto)
        {
            string query = "Update FeatureCard Set Icon=@Icon ,SubTitle=@SubTitle,Title=@Title,Description=@Description,ButtonText=@ButtonText,ButtonUrl=@ButtonUrl Where FeatureCardId=@id";
            var parameters = new DynamicParameters();
            parameters.Add("@Icon", dto.Icon);
            parameters.Add("@id", dto.FeatureCardId);
            parameters.Add("@SubTitle", dto.SubTitle);
            parameters.Add("@Title", dto.Title);
            parameters.Add("@Description", dto.Description);
            parameters.Add("@ButtonText", dto.ButtonText);
            parameters.Add("@ButtonUrl", dto.ButtonUrl);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
