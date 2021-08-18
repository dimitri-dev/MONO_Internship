using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using _003___Baze_podataka.Models.Student;

namespace _003___Baze_podataka.Models.Car
{
    public class Car
    {
        private Guid _id;
        private string _registration;
        private Guid _studentId;
        private Student.Student _student;

        [DataMember]
        public Guid Id
        {
            get => _id;
            set => _id = value;
        }

        [DataMember]
        public string Registration
        {
            get => _registration;
            set => _registration = value;
        }

        [DataMember]
        public Guid StudentID
        {
            get => _studentId;
            set => _studentId = value;
        }

        [DataMember]
        public Student.Student Student
        {
            get => _student;
            set => _student = value;
        }

        // Needed for Serialization
        public Car() { }

        public Car(string registration)
        {
            _registration = registration;
            _id = Guid.NewGuid();
        }
    }
}