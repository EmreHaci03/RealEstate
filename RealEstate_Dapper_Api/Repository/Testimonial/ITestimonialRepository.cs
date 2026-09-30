using RealEstate_Dapper_Api.DTOS.TestimonialDtos;

namespace RealEstate_Dapper_Api.Repository.Testimonial
{
    public interface ITestimonialRepository
    {
        Task<List<ResultTestimonialDto>> GetAllAsync();
        Task<List<ResultTestimonialDto>> GetActiveTestimonialsAsync();
        Task CreateAsync(CreateTestimonialDto dto);

        Task ChangeTestimonialStatus(int id, bool status);
        Task DeleteAsync(int id);
        Task UpdateAsync(UpdateTestimonialDto dto);
        Task<GetTestimonialByIdDto> GetByIdAsync(int id);
    }
}
