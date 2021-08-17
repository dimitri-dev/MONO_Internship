using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _002___WebAPI.Models
{
    public interface IStudentRepository
    {
        IEnumerable<Student> GetAll();
        Student Get(Guid id);
        Student Add(Student item);
        void Remove(Guid id);
        bool Update(Student item);
    }
}
