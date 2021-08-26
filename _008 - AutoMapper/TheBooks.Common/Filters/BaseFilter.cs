using TheBooks.Common.IFilters;

namespace TheBooks.Common.Filters
{
    public class BaseFilter : IBaseFilter
    {
        #nullable enable
        public string? Search { get; set; }
        #nullable disable
    }
}
