using TheBooks.Common.IFilters;

namespace TheBooks.Common.Filters
{
    public class StudentFilter : BaseFilter, IStudentFilter
    {
        #nullable enable
        public string? Gender { get; set; }
        #nullable disable
    }
}
