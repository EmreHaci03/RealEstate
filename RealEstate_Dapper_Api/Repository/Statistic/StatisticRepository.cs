using Dapper;
using RealEstate_Dapper_Api.Context;
using RealEstate_Dapper_Api.DTOS.StatisticDtos;

namespace RealEstate_Dapper_Api.Repository.Statistic
{
    public class StatisticRepository : IStatisticRepository
    {
        private readonly DapperContext _context;

        public StatisticRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<ResultAgentProductCountByCityDto>> GetAgentProductCountByCityAsync(int userId)
        {
            string query = @"
            DECLARE @EmployeeId int=(select EmployeeId From Employee e inner join AppUser u on e.Mail=u.Email Where u.UserId=@UserId);

SELECT Count(*) as ProductCount ,City From Product Where Employee=@EmployeeId Group By City Order By ProductCount Desc ";

            var parameters = new DynamicParameters();
            parameters.Add("@UserId", userId);
            using(var connection = _context.CreateConnection())
            {
                var values= await connection.QueryAsync<ResultAgentProductCountByCityDto>(query, parameters);
                return values.ToList();    
            }
        }

        public async Task<ResultAgentStatisticDto> GetAgentStatisticsAsync(int userId)
        {
            string query = @"
    DECLARE @EmployeeId INT = (
        SELECT e.EmployeeId
        FROM Employee e
        INNER JOIN AppUser u ON e.Mail = u.Email
        WHERE u.UserId = @UserId
    );

    SELECT
        (SELECT COUNT(*) FROM Product WHERE Employee = @EmployeeId) AS TotalProduct,
        (SELECT ISNULL(SUM(Price), 0) FROM Product WHERE Employee = @EmployeeId) AS TotalProductValue,
        (SELECT COUNT(DISTINCT Category) FROM Product WHERE Employee = @EmployeeId) AS DifferentCategoryValue,
        (SELECT COUNT(*) + 1
        FROM (SELECT Employee, COUNT(*) AS Cnt FROM Product GROUP BY Employee) AS Ranks
        WHERE Ranks.Cnt > (SELECT COUNT(*) FROM Product WHERE Employee = @EmployeeId)) AS EstateAgentRanking";
            

            var parameters = new DynamicParameters();
            parameters.Add("@UserId", userId);

            using(var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstAsync<ResultAgentStatisticDto>(query,parameters);
            }

        }

        public async Task<List<ResultAgentAveragePriceByCategoryDto>> GetAveragePriceByCategoryAsync(int userId)
        {
            string query = @"

 Declare @EmployeeId int=(
select EmployeeId From Employee e inner join AppUser a on e.Mail=a.Email
        WHERE a.UserId = @UserId);
                SELECT avg(Price) as AveragePrice , c.CategoryName as CategoryName   From Product p inner join Category c on p.Category=c.CategoryId Where Employee=@EmployeeId Group By CategoryName
                ";

            var parameters = new DynamicParameters();
            parameters.Add("@UserId", userId);
            using(var connection = _context.CreateConnection())
            {
                var values= await connection.QueryAsync<ResultAgentAveragePriceByCategoryDto>(query,parameters);
                return values.ToList();
            }
        }

        public async Task<ResultStatisticDto> GetStatisticsAsync()
        {
            string query = @"
                SELECT
                    (SELECT COUNT(*) FROM Product) AS TotalProduct,
                    (SELECT COUNT(*) FROM Category) AS TotalCategory,
                    (SELECT COUNT(*) FROM Employee) AS TotalEmployee,
                    (SELECT COUNT(*) FROM Employee WHERE Status = 1) AS ActiveEmployee,
                    (SELECT COUNT(DISTINCT City) FROM Product) AS TotalCity,
                    (SELECT ISNULL(AVG(Price), 0) FROM Product) AS AveragePrice,
                    (SELECT ISNULL(MAX(Price), 0) FROM Product) AS MaxPrice,
                    (SELECT ISNULL(MIN(Price), 0) FROM Product) AS MinPrice,

                    (SELECT TOP 1 City FROM Product
                     GROUP BY City ORDER BY COUNT(*) DESC) AS TopCity,

                    (SELECT TOP 1 c.CategoryName FROM Product p
                     INNER JOIN Category c ON p.Category = c.CategoryId
                     GROUP BY c.CategoryName ORDER BY COUNT(*) DESC) AS TopCategory,

                    (SELECT TOP 1 e.Name FROM Product p
                     INNER JOIN Employee e ON p.Employee = e.EmployeeId
                     GROUP BY e.Name ORDER BY COUNT(*) DESC) AS TopEmployee,

                    (SELECT COUNT(*) FROM Subscriber WHERE IsActive = 1) AS ActiveSubscriber";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstAsync<ResultStatisticDto>(query);
            }
        }

        public async Task<List<ResultAgentProductCountByCategoryDto>> ResultAgentProductCountByCategoryAsync(int userId)
        {
            string query = @"
             DECLARE @EmployeeId INT = (
        SELECT e.EmployeeId
        FROM Employee e
        INNER JOIN AppUser u ON e.Mail = u.Email
        WHERE u.UserId = @UserId
    );
                Select Count(*) As ProductCount,c.CategoryName as CategoryName From Product p inner join Category c on p.Category=c.CategoryId Where Employee=@EmployeeId Group By CategoryName ";
        var parameters=new DynamicParameters();    
            parameters.Add("@UserId",userId);
        using(var connection = _context.CreateConnection())
            {
                var values= await connection.QueryAsync<ResultAgentProductCountByCategoryDto>(query, parameters);
                return values.ToList();
            }
        }
    }
}