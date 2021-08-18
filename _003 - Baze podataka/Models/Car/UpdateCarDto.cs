using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _003___Baze_podataka.Models.Car
{
    public class UpdateCarDto
    {
        #nullable enable
        public string? Registration { get; set; }
        public Guid? StudentId { get; set; }
        #nullable disable
    }
}