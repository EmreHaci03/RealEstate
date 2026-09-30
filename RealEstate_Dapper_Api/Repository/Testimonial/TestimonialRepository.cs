using Dapper;
using RealEstate_Dapper_Api.Context;
using RealEstate_Dapper_Api.DTOS.TestimonialDtos;

namespace RealEstate_Dapper_Api.Repository.Testimonial
{
    public class TestimonialRepository : ITestimonialRepository
    {
        private readonly DapperContext _context;

        public TestimonialRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task ChangeTestimonialStatus(int id, bool status)
        {
            string query = "Update Testimonial Set Status = @Status Where TestimonialId = @id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            parameters.Add("@Status", status);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task CreateAsync(CreateTestimonialDto dto)
        {
            string query = "Insert Into Testimonial (NameSurname, Title, Comment, Status) Values (@NameSurname, @Title, @Comment, @Status)";
            var parameters = new DynamicParameters();
            parameters.Add("@NameSurname", dto.NameSurname);
            parameters.Add("@Title", dto.Title);
            parameters.Add("@Comment", dto.Comment);
            parameters.Add("@Status", dto.Status);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async Task DeleteAsync(int id)
        {
            string query = "Delete From Testimonial Where TestimonialId = @id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultTestimonialDto>> GetActiveTestimonialsAsync()
        {
            string query = "Select * From Testimonial Where Status = 1";

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultTestimonialDto>(query);
                return values.ToList();
            }
        }

        public async Task<List<ResultTestimonialDto>> GetAllAsync()
        {
            string query = "Select * From Testimonial";

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultTestimonialDto>(query);
                return values.ToList();
            }
        }

        public async Task<GetTestimonialByIdDto> GetByIdAsync(int id)
        {
            string query = "Select * From Testimonial Where TestimonialId = @id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<GetTestimonialByIdDto>(query, parameters);
            }
        }

        public async Task UpdateAsync(UpdateTestimonialDto dto)
        {
            string query = "Update Testimonial Set NameSurname = @NameSurname, Title = @Title, Comment = @Comment, Status = @Status Where TestimonialId = @id";
            var parameters = new DynamicParameters();
            parameters.Add("@NameSurname", dto.NameSurname);
            parameters.Add("@Title", dto.Title);
            parameters.Add("@Comment", dto.Comment);
            parameters.Add("@Status", dto.Status);
            parameters.Add("@id", dto.TestimonialId);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}