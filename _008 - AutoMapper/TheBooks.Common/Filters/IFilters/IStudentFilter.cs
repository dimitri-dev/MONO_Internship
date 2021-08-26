using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBooks.Common.IFilters
{
    public interface IStudentFilter : IBaseFilter
    {
        #nullable enable
        string? Gender { get; set; }
        #nullable disable
    }
}
