using System;

namespace TheBooks.Models.Common
{
    public interface IStudent
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Surname { get; set; }
        string Gender { get; set; }
    }
}
