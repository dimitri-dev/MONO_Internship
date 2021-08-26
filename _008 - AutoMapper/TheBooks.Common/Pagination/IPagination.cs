using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBooks.Common.Pagination
{
    public interface IPagination
    {
        #nullable enable
        int? PageNumber { get; set; }
        int? PageSize { get; set; }
        #nullable disable
    }
}
