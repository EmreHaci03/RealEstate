using Microsoft.Extensions.Primitives;

namespace RealEstate_Dapper_Api.DTOS.BottomGridDtos
{
    public class GetBottomGridByIdDto
    {
        public int BottomGridId { get; set; }
        public string Icon { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
