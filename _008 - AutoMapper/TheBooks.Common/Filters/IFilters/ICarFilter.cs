using System;

namespace TheBooks.Common.IFilters
{
    public interface ICarFilter : IBaseFilter
    {
        #nullable enable
        Guid? StudentID { get; set; }
        #nullable disable
    }
}
