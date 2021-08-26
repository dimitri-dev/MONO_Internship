using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheBooks.Models;
using TheBooks.Models.Common;

namespace TheBooks.Mapper
{
    public class RestProfile : Profile
    {
        public RestProfile()
        {
            CreateMap<IStudent, Controllers.REST_Student.StudentRest>();
            CreateMap<Controllers.REST_Student.CreateStudentRest, Student>();
            CreateMap<Controllers.REST_Student.UpdateStudentRest, Student>().ForAllMembers(o => o.Condition((dto, student, member) => member != null));

            CreateMap<ICar, Controllers.REST_Car.CarRest>();
            CreateMap<Controllers.REST_Car.CreateCarRest, Car>();
            CreateMap<Controllers.REST_Car.UpdateCarRest, Car>().ForAllMembers(o => o.Condition((dto, car, member) => member != null));

        }
    }
}