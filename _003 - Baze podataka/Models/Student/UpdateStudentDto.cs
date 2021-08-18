using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _003___Baze_podataka.Models.Student
{
    public class UpdateStudentDto
    {
        #nullable enable
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Gender { get; set; }
        #nullable disable
    }
}