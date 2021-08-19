using System;
using System.ComponentModel.DataAnnotations;
using TheBooks.Models.Common;

namespace TheBooks.Models
{
    public class CreateCarDto : ICreateCarDto
    {
        [Required]
        public string Registration { get; set; }

        [Required]
        public Guid StudentId { get; set; }
    }
}
