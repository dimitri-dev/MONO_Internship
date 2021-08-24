using System;
using TheBooks.Models.Common;

namespace TheBooks.Models
{
    public class UpdateCarDto : IUpdateCarDto
    {
        #nullable enable
        public string? Registration { get; set; }
        public Guid? StudentId { get; set; }
        #nullable disable
    }
}
