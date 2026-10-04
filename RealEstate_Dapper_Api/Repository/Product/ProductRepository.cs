using Dapper;
using RealEstate_Dapper_Api.Context;
using RealEstate_Dapper_Api.DTOS.ProductDetailDtos;
using RealEstate_Dapper_Api.DTOS.ProductDtos;

namespace RealEstate_Dapper_Api.Repository.Product
{
    public class ProductRepository : IProductRepository
    {
        private readonly DapperContext _context;

        public ProductRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(CreateProductDto dto)
        {
            string query = "insert into Product(Title,Price,CoverImage,City,District,Adress,Description,Category,Employee) " +
                 "values(@Title,@Price,@CoverImage,@City,@District,@Adress,@Description,@Category,@Employee)";
            var parameters = new DynamicParameters();
            parameters.Add("@Title",dto.Title);
            parameters.Add("@Price",dto.Price);
            parameters.Add("@CoverImage",dto.CoverImage);
            parameters.Add("@City",dto.City);
            parameters.Add("@District",dto.District);
            parameters.Add("@Adress",dto.Adress);
            parameters.Add("@Description",dto.Description);
            parameters.Add("@Category",dto.Category);
            parameters.Add("@Employee",dto.Employee);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query,parameters);
            }
        }

        public async Task DeleteAsync(int id)
        {
            string query = "Delete From Product Where ProductId=@id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultProductDto>> GetAllAsync()
        {
            string query = "Select * From Product";
            var parameters = new DynamicParameters();
            using (var connection = _context.CreateConnection())
            {
                var values=await connection.QueryAsync<ResultProductDto>(query, parameters);
                return values.ToList();
            }
        }

        public async Task<GetProductByIdDto> GetByIdAsync(int id)
        {
            string query = "Select p.ProductId, p.Title, p.Price, p.CoverImage, p.City, p.District, p.Adress, p.Description,c.CategoryName AS CategoryName, e.Name AS EmployeeName\r\nFrom Product p\r\nInner Join Category c On p.Category = c.CategoryId\r\nInner Join Employee e On p.Employee = e.EmployeeId\r\nWhere p.ProductId = @id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<GetProductByIdDto>(query, parameters);
                return values;
            }
        }

        public async Task<List<ResultProductWithCategoryDto>> GetProductAdvertsListByEmployeeAsync(int id)
        {
            string query = "Select ProductId,Title,Price,City,District,Description,CoverImage,Adress,c.CategoryName From Product p inner join Category c on p.Category=c.CategoryId Where Employee=@Employee";
            var parameters = new DynamicParameters();
            parameters.Add("@Employee", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductWithCategoryDto>(query,parameters);
                return values.ToList();
            }
        }

        public async Task<List<ResultProductWithCategoryDto>> GetProductAdvertsListByUserAsync(int userId)
        {
            string query = @"SELECT p.ProductId, p.Title, p.Price, p.City, p.District, p.Description,
                            p.CoverImage, p.Adress, c.CategoryName
                     FROM Product p
                     INNER JOIN Category c ON p.Category = c.CategoryId
                     INNER JOIN Employee e ON p.Employee = e.EmployeeId
                     INNER JOIN AppUser u ON e.Mail = u.Email
                     WHERE u.UserId = @UserId";
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", userId);

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductWithCategoryDto>(query, parameters);
                return values.ToList();
            }
        }

        public async Task<List<ResultProductWithCategoryAndEmployeeDto>> ResultProductWithCategoryAndEmployeeAsync()
        {
            string query = "Select ProductId,p.Title,p.Price,p.City,p.District,p.Adress,p.Description,CoverImage,c.CategoryName as CategoryName,e.Name as EmployeeName From Product p inner join Category c on p.Category=c.CategoryId\r\ninner join Employee e on p.Employee=e.EmployeeId";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductWithCategoryAndEmployeeDto>(query);
                return values.ToList();
            }
        }

        public async Task<List<ResultProductWithCategoryDto>> ResultProductWithCategoryAsync()
        {
            string query = "Select ProductId,Title,Price,City,District,Description,CoverImage,Adress,c.CategoryName From Product p inner join Category c on p.Category=c.CategoryId";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductWithCategoryDto>(query);
                return values.ToList();
            }
        }

        public async Task<List<ResultProductCountByCityDto>> ResultProductWithCityCount()
        {
            string query = "select city,MAX(CoverImage) AS CoverImage, Count(*) AS ProductCount From Product \r\ngroup by City\r\norder by  Count(*) desc";
            
            using(var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductCountByCityDto>(query);
                return values.ToList();
            }
        }

        public async Task UpdateAsync(UpdateProductDto dto)
        {
            string query = "update Product Set Title=@Title, Price=@Price, CoverImage=@CoverImage, City=@City, District=@District, Adress=@Adress" +
    ", Description=@Description, Category=@Category, Employee=@Employee Where ProductId=@ProductId";
            var parameters = new DynamicParameters();
            parameters.Add("@Title", dto.Title);
            parameters.Add("@Price", dto.Price);
            parameters.Add("@CoverImage", dto.CoverImage);
            parameters.Add("@City", dto.City);
            parameters.Add("@District", dto.District);
            parameters.Add("@Adress", dto.Adress);
            parameters.Add("@ProductId", dto.ProductId);
            parameters.Add("@Description", dto.Description);
            parameters.Add("@Category", dto.Category);
            parameters.Add("@Employee", dto.Employee);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
