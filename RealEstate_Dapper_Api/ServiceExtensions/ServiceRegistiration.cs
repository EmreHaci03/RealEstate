using RealEstate_Dapper_Api.Repository.Category;
using RealEstate_Dapper_Api.Repository.Employee;
using RealEstate_Dapper_Api.Repository.Product;
using RealEstate_Dapper_Api.Repository.ProductDetail;
using RealEstate_Dapper_Api.Repository.ProductDetailDto;

namespace RealEstate_Dapper_Api.ServiceExtensions
{
    public static class ServiceRegistiration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryRepository,CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IProductDetailRepository, ProductDetailRepository>();
            return services;
        }
    }
}
