using RealEstate_Dapper_Api.DTOS.ContactDtos;

namespace RealEstate_Dapper_Api.Repository.Contact
{
    public interface IContactRepository
    {
        Task<List<ResultContactDto>> GetAllAsync();
        Task<GetContactByIdDto> GetByIdAsync(int id);

        Task CreateAsync(CreateContactDto dto);
        Task DeleteAsync(int id);
    }
}
