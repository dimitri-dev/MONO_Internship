using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBooks.Common.Sort
{
    public interface ISort
    {
        #nullable enable
        string? Order { get; set; }
        string? SortBy { get; set; }
        #nullable disable
    }
}
