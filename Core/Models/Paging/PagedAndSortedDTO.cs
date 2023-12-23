namespace GreatProj.Core.Models.Paging
{
    public class PagedAndSortedDTO : PagedDTO
    {
        public string? Sorting { get; set; }
        public bool IsAscending { get; set; }
    }
}
