using System;
using System.Runtime.Serialization;
using TheBooks.Models.Common;

namespace TheBooks.Models
{
    public class Student : IStudent
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
    }
}
