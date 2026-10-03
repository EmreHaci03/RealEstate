using RealEstate_Dapper_Api.DTOS.DashboardDtos;

namespace RealEstate_Dapper_Api.Repository.Dashboard
{
    public interface IDashboardRepository
    {
        Task<ResultDashboardSummaryDto> GetSummaryAsync();
        Task<List<ResultEmployePerformanceDto>> GetEmployeePerformance();
        Task<List<ResultProductCountByCategoryDto>> GetProductCountByCategoryAsync();

        Task<List<ResultDashboardDayMessageDto>> GetDashboardDayMessageAsync();

        Task<List<ResultDashboardProductByCityDto>> GetProductCountByCityAsync();

        Task<List<ResultDashboardAveragePriceByCategoryDto>> GetAveragePriceByCategory();
    }
}
