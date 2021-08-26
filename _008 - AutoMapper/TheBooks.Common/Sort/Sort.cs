using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBooks.Common.Sort
{
    public class Sort : ISort
    {
        #nullable enable
        public string? Order { get; set; } = "ASC";
        public string? SortBy { get; set; }
        #nullable disable
    }
}
