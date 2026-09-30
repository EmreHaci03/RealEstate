using RealEstate_Dapper_Api.Repository.Adress;
using RealEstate_Dapper_Api.Repository.BottomGrid;
using RealEstate_Dapper_Api.Repository.Category;
using RealEstate_Dapper_Api.Repository.Client;
using RealEstate_Dapper_Api.Repository.Contact;
using RealEstate_Dapper_Api.Repository.Employee;
using RealEstate_Dapper_Api.Repository.FeatureCardDto;
using RealEstate_Dapper_Api.Repository.Product;
using RealEstate_Dapper_Api.Repository.ProductDetail;
using RealEstate_Dapper_Api.Repository.ProductDetailDto;
using RealEstate_Dapper_Api.Repository.Service;
using RealEstate_Dapper_Api.Repository.SocialMedia;
using RealEstate_Dapper_Api.Repository.Subscriber;
using RealEstate_Dapper_Api.Repository.Testimonial;
using RealEstate_Dapper_Api.Repository.WhoWeAre;

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
            services.AddScoped<IWhoWeAreRepository, WhoWeAreRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IBottomGridRepository, BottomGridRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IFeatureCardRepository, FeatureCardRepository>();
            services.AddScoped<ISubscriberRepository, SubscriberRepository>();
            services.AddScoped<ITestimonialRepository, TestimonialRepository>();
            services.AddScoped<ISocialMediaRepository, SocialMediaRepository>();
            services.AddScoped<IAdressRepository, AdressRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            return services;
        }
    }
}
