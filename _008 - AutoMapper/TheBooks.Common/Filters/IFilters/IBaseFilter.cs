namespace TheBooks.Common.IFilters
{
    public interface IBaseFilter
    {
        #nullable enable
        string? Search { get; set; }
        #nullable disable
    }
}
