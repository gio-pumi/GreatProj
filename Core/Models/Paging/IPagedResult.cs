namespace GreatProj.Core.Models.Paging
{
    public interface IPagedResult <T>
    {
        IReadOnlyList<T> Items { get; set; }
    }
}
