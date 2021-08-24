using System;

namespace TheBooks.Models.Common
{
    public interface ICreateCarDto
    {
        string Registration { get; set; }
        Guid StudentId { get; set; }
    }
}
