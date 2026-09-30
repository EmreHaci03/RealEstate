using RealEstate_Dapper_Api.DTOS.FeatureCardDtos;

namespace RealEstate_Dapper_Api.Repository.FeatureCardDto
{
    public interface IFeatureCardRepository
    {
        Task<List<ResultFeatureCardDto>> GetAllAsync();

        Task CreateAsync(CreateFeatureCardDto dto);
        Task DeleteAsync(int id);
        Task UpdateAsync(UpdateFeatureCardDto dto);

        Task<GetFeatureCardByIdDto> GetByIdAsync(int id);
    }
}
