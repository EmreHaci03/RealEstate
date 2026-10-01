using RealEstate_Dapper_Api.DTOS.StatisticDtos;

namespace RealEstate_Dapper_Api.Repository.Statistic
{
    public interface IStatisticRepository
    {
        Task<ResultStatisticDto> GetStatisticsAsync();
    }
}
