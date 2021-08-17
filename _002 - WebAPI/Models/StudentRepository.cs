using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _002___WebAPI.Models
{
    public class StudentRepository : IStudentRepository
    {
        private List<Student> _students = new List<Student>();

        public StudentRepository()
        {
            Add(new Student("Jovan", "Mirkec"));
            Add(new Student("Jelena", "Klobuk"));
            Add(new Student("Milan", "Bandic"));
        }

        public Student Add(Student item)
        {
            if (item == null) throw new ArgumentNullException("item");

            if (_students.Count(person => person.Id == item.Id) > 0) throw new ArgumentException("in use");

            _students.Add(item);
            return item;
        }

        public Student Get(Guid id)
        {
            return _students.Find(person => person.Id == id);
        }

        public IEnumerable<Student> GetAll()
        {
            return _students;
        }

        public void Remove(Guid id)
        {
            _students.RemoveAll(person => person.Id == id);
        }

        public bool Update(Student item)
        {
            if (item == null) throw new ArgumentNullException("item");

            int idx = _students.FindIndex(person => person.Id == item.Id);
            if (idx == -1) return false;

            _students.RemoveAt(idx);
            _students.Add(item);
            return true;
        }
    }
}