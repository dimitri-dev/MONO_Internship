using System;

namespace TheBooks.Models.Common
{
    public interface ICar
    {
        Guid Id { get; set; }
        string Registration { get; set; }
        Guid StudentID { get; set; }
        IStudent Student { get; set; }
    }
}
