using RealEstate_Dapper_Api.DTOS.SocialMediaDtos;

namespace RealEstate_Dapper_Api.Repository.SocialMedia
{
    public interface ISocialMediaRepository
    {
        Task<List<ResultSocialMediaDto>> GetAllAsync();
        Task CreateAsync(CreateSocialMediaDto dto);
        Task DeleteAsync(int id);
        Task UpdateAsync(UpdateSocialMediaDto dto);
        Task<GetSocialMediaByIdDto> GetByIdAsync(int id);
    }
}
