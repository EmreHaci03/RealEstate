using RealEstate_Dapper_Api.DTOS.StatisticDtos;

namespace RealEstate_Dapper_Api.Repository.Statistic
{
    public interface IStatisticRepository
    {
        Task<ResultStatisticDto> GetStatisticsAsync();
        Task<List<ResultAgentAveragePriceByCategoryDto>> GetAveragePriceByCategoryAsync(int userId);

        Task<List<ResultAgentProductCountByCityDto>> GetAgentProductCountByCityAsync(int userId);

        Task<List<ResultAgentProductCountByCategoryDto>> ResultAgentProductCountByCategoryAsync(int userId);

        Task<ResultAgentStatisticDto> GetAgentStatisticsAsync(int userId);
    }
}
