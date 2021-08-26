using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBooks.Common.Pagination
{
    public class Pagination : IPagination
    {
        #nullable enable
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; } = 15;
        #nullable disable
    }
}
