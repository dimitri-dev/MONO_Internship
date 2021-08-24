using System;

namespace TheBooks.Models.Common
{
    public interface IUpdateCarDto
    {
        #nullable enable
        string? Registration { get; set; }
        Guid? StudentId { get; set; }
        #nullable disable
    }
}
