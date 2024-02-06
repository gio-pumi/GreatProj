using GreatProj.Core.Models.Paging;
namespace GreatProj.Core.Models.Country
{
    public class GetAllCountryInput : PagedAndSortedDTO
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
    }
}
