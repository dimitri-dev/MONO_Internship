using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace _002___WebAPI.Models
{
    [DataContract]
    public class Student
    {
        private Guid _id;
        private string _name;
        private string _surname;

        [DataMember]
        public Guid Id
        {
            get => _id;
            set => _id = value;
        }

        [DataMember]
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        [DataMember]
        public string Surname
        {
            get => _surname;
            set => _surname = value;
        }

        public void SetNewGuid(Guid id) => _id = id;

        // Needed for Serialization
        public Student() { }

        public Student(string firstName, string lastName)
        {
            _name = firstName;
            _surname = lastName;
            _id = Guid.NewGuid();
        }
    }
}