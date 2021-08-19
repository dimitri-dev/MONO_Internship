using TheBooks.Models.Common;

namespace TheBooks.Models
{
    public class UpdateStudentDto : IUpdateStudentDto
    {
        #nullable enable
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Gender { get; set; }
        #nullable disable
    }
}
