﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBooks.Models.Common
{
    public interface ICreateStudentDto
    {
        string Name { get; set; }
        string Surname { get; set; }
        string Gender { get; set; }
    }
}
