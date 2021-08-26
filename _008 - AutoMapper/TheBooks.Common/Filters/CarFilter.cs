using System;
using TheBooks.Common.IFilters;

namespace TheBooks.Common.Filters
{
    public class CarFilter : BaseFilter, ICarFilter
    {
        #nullable enable
        public Guid? StudentID { get; set; }
        #nullable disable
    }
}
