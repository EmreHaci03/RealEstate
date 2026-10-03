using Dapper;
using RealEstate_Dapper_Api.Context;
using RealEstate_Dapper_Api.DTOS.DashboardDtos;

namespace RealEstate_Dapper_Api.Repository.Dashboard
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly DapperContext _context;

        public DashboardRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<ResultDashboardAveragePriceByCategoryDto>> GetAveragePriceByCategory()
        {
            string query = "SELECT AVG(Price) as AveragePrice,c.CategoryName AS CategoryName , COUNT(*) as TotalProductCount FROM Product p inner join Category c on p.Category=c.CategoryId group by c.CategoryName  order by AVG(Price)";

            using(var connection = _context.CreateConnection())
            {
                var values= await connection.QueryAsync<ResultDashboardAveragePriceByCategoryDto>(query);
                return values.ToList();
            }
        }

        public async Task<List<ResultDashboardDayMessageDto>> GetDashboardDayMessageAsync()
        {
            string query = @"
    SELECT FORMAT(CAST(SendDate AS DATE), 'dd.MM') AS Day, COUNT(*) AS MessageCount
    FROM Contact
    WHERE SendDate >= DATEADD(DAY, -6, CAST(GETDATE() AS DATE))
    GROUP BY CAST(SendDate AS DATE)
    ORDER BY CAST(SendDate AS DATE)";

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultDashboardDayMessageDto>(query);
                return values.ToList();
            }
        }

        public async Task<List<ResultEmployePerformanceDto>> GetEmployeePerformance()
        {
            string query = "select e.Name as EmployeeName,count(*) as PropertyCount from Product p inner join Employee e  on p.Employee=e.EmployeeId group by  e.Name order by PropertyCount desc";

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultEmployePerformanceDto>(query);
                return values.ToList();
            }
        }

        public async Task<List<ResultProductCountByCategoryDto>> GetProductCountByCategoryAsync()
        {
            string query = "select c.CategoryName as CategoryName,count(*) as ProductCount from Product p inner join Category c on p.Category=c.CategoryId group by c.CategoryName";

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductCountByCategoryDto>(query);
                return values.ToList();
            }
        }

        public async Task<List<ResultDashboardProductByCityDto>> GetProductCountByCityAsync()
        {
            string query = "Select  city as City,Count(*) as ProductCount  FROM Product Group By City Order By ProductCount desc"   ;

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultDashboardProductByCityDto>(query);
                return values.ToList();
            }
        }

        public async Task<ResultDashboardSummaryDto> GetSummaryAsync()
        {
            string query = @"
    SELECT
        (SELECT COUNT(*) FROM Product) AS TotalProduct,
        (SELECT ISNULL(SUM(Price), 0) FROM Product) AS TotalProductValue,
        (SELECT COUNT(*) FROM Subscriber WHERE IsActive = 1) AS ActiveSubscribers,
        (SELECT COUNT(*) FROM Contact
         WHERE MONTH(SendDate) = MONTH(GETDATE())
           AND YEAR(SendDate) = YEAR(GETDATE())) AS MonthlyMessage";

            using(var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<ResultDashboardSummaryDto>(query);;
            }
        }
    }
}
