using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _003___Baze_podataka.Models.Car
{
    public class CreateCarDto
    {
        [Required]
        public string Registration { get; set; }

        [Required]
        public Guid StudentId { get; set; }
    }
}