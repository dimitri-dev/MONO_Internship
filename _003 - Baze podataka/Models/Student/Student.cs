using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace _003___Baze_podataka.Models.Student
{
    [DataContract]
    public class Student
    {
        private Guid _id;
        private string _name;
        private string _surname;
        private string _gender;

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

        [DataMember]
        public string Gender
        {
            get => _gender;
            set => _gender = value;
        }

        // Needed for Serialization
        public Student() { }

        public Student(string firstName, string lastName, string gender)
        {
            _name = firstName;
            _surname = lastName;
            _gender = gender;
            _id = Guid.NewGuid();
        }
    }
}