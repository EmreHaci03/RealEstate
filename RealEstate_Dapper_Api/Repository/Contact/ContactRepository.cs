using Dapper;
using RealEstate_Dapper_Api.Context;
using RealEstate_Dapper_Api.DTOS.ContactDtos;

namespace RealEstate_Dapper_Api.Repository.Contact
{
    public class ContactRepository: IContactRepository
    {
        private readonly DapperContext _context;

        public ContactRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(CreateContactDto dto)
        {
            string query = "Insert Into Contact (Name, Subject, Mail, Message, SendDate) Values (@Name, @Subject, @Mail, @Message, GETDATE())";
            var parameters = new DynamicParameters();
            parameters.Add("@Name",dto.Name);
            parameters.Add("@Subject", dto.Subject);
            parameters.Add("@Mail", dto.Mail);
            parameters.Add("@Message", dto.Message);

            using(var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteAsync(int id)
        {
            string query = "Delete From Contact Where ContactId=@id";
            var paremeters = new DynamicParameters();
            paremeters.Add("@id", id);

            using(var connection = _context.CreateConnection())
            {
                 await connection.ExecuteAsync(query, paremeters);
            }
        }

        public async Task<List<ResultContactDto>> GetAllAsync()
        {
            string query = "Select *From Contact";

            using (var connection = _context.CreateConnection())
            {
                var values= await connection.QueryAsync<ResultContactDto>(query);
                return values.ToList();
            }
        }

        public async Task<GetContactByIdDto> GetByIdAsync(int id)
        {
            string query = "Select *From Contact Where ContactId=@id";
            var parameters= new DynamicParameters();
            parameters.Add("@id", id);

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<GetContactByIdDto>(query,parameters);
         
            }
        }
    }
}
