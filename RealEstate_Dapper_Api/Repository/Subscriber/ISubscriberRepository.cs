using RealEstate_Dapper_Api.DTOS.SubscriberDtos;

namespace RealEstate_Dapper_Api.Repository.Subscriber
{
    public interface ISubscriberRepository
    {
        Task<bool> EmailExistsAsync(string email);
        Task DeleteAsync(int id);
        Task ChangeSubscribeStatusAsync(int id, bool isActive);
        Task CreateAsync(CreateSubscriberDto dto);
        Task<List<ResultSubscriberDto>> GetAllAsync();
        Task<GetSubscriberByIdDto> GetByIdAsync(int id);
    }
}
