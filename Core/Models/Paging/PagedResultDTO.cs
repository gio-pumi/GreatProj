namespace GreatProj.Core.Models.Paging
{
    public class PagedResultDTO<T> : IPagedResult<T>
    {
        public  IReadOnlyList<T> Items { get; set; }
        public int Count { get; set; }
    }
}
 