using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _003___Baze_podataka.Models.Common
{
    public class BaseQueryDto
    {
        #nullable enable
        public string? Search { get; set; }
        public string? SortBy { get; set; }
        public string? Order { get; set; }
        #nullable disable
    }
}