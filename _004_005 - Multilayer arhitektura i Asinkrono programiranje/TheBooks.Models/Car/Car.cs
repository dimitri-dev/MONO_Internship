using System;
using System.Runtime.Serialization;
using TheBooks.Models.Common;

namespace TheBooks.Models
{
    public class Car : ICar
    {
        private Guid _id;
        private string _registration;
        private Guid _studentId;
        private IStudent _student;

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
        public IStudent Student
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
