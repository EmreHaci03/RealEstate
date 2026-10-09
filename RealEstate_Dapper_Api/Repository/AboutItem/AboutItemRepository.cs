using Dapper;
using RealEstate_Dapper_Api.Context;
using RealEstate_Dapper_Api.DTOS.AboutItemDtos;

namespace RealEstate_Dapper_Api.Repository.AboutItem
{
    public class AboutItemRepository : IAboutItemRepository
    {
        private readonly DapperContext _context;

        public AboutItemRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(CreateAboutItemDto dto)
        {
            string query = "insert into AboutItem(Icon,Title,Content,About) values(@Icon,@Title,@Content,@About)";
            var parameters = new DynamicParameters();
            parameters.Add("@Icon", dto.Icon);
            parameters.Add("@Title", dto.Title);
            parameters.Add("@Content", dto.Content);
            parameters.Add("@About", dto.About);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteAsync(int id)
        {
            string query = "Delete AboutItem Where AboutItemId=@AboutItemId";
            var parameters = new DynamicParameters();
            parameters.Add("@AboutItemId", id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<GetAboutItemByIdDto> GetAboutItemByIdDto(int id)
        {
            string query = @"Select i.AboutItemId, i.Icon, i.Title, i.Content, a.Title as About
                             From AboutItem i
                             Inner Join About a On i.About = a.AboutId
                             Where i.AboutItemId = @AboutItemId";
            var parameters = new DynamicParameters();
            parameters.Add("@AboutItemId", id);
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<GetAboutItemByIdDto>(query, parameters);
            }
        }

        public async Task<List<GetAboutItemByIdDto>> GetAboutItemsByAboutAsync(int aboutId)
        {
            string query = @"Select i.AboutItemId, i.Icon, i.Title, i.Content, a.Title as About
                             From AboutItem i
                             Inner Join About a On i.About = a.AboutId
                             Where i.About = @AboutId";
            var parameters = new DynamicParameters();
            parameters.Add("@AboutId", aboutId);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<GetAboutItemByIdDto>(query, parameters);
                return values.ToList();
            }
        }

        public async Task<List<ResultAboutItemDto>> GetAllAsync()
        {
            string query = @"Select i.AboutItemId, i.Icon, i.Title, i.Content, a.Title as About
                             From AboutItem i
                             Inner Join About a On i.About = a.AboutId";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultAboutItemDto>(query);
                return values.ToList();
            }
        }

        public async Task UpdateAsync(UpdateAboutItemDto dto)
        {
            string query = "Update AboutItem Set Icon=@Icon, Title=@Title, Content=@Content, About=@About Where AboutItemId=@AboutItemId";
            var parameters = new DynamicParameters();
            parameters.Add("@Icon", dto.Icon);
            parameters.Add("@Title", dto.Title);
            parameters.Add("@Content", dto.Content);
            parameters.Add("@About", dto.About);
            parameters.Add("@AboutItemId", dto.AboutItemId);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}