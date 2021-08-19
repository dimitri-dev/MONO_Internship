using System.ComponentModel.DataAnnotations;
using TheBooks.Models.Common;

namespace TheBooks.Models
{
    public class CreateStudentDto : ICreateStudentDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Gender { get; set; }
    }
}
