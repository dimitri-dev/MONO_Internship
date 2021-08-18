using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _003___Baze_podataka.Models.Student;

namespace _003___Baze_podataka.Repositories
{
    public interface IStudentRepository
    {
        Student Create(CreateStudentDto dto);
        IEnumerable<Student> GetAll();
        Student Get(Guid id);
        Student Update(Guid id, UpdateStudentDto dto);
        void Delete(Guid id);
    }
}