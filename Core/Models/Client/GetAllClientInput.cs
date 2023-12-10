using GreatProj.Core.Models.Paging;

namespace GreatProj.Core.Models.Client
{
    public class GetAllClientInput : PagedAndSortedDTO
    {
        public string? RoomNumber { get; set; }
        public decimal? Balance { get; set; }
        public string? Mail { get; set; }
        public string? Number { get; set; }
        public string? PersonalNumber { get; set; }
        public DateTime? StartDate { get; set; } 
        public DateTime? EndDate { get; set; } 
    }
}
