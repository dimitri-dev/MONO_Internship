using AutoMapper;
using AutoMapper.Data.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBooks.Models;

namespace TheBooks.Repository
{
    public class RepositoryProfile : Profile
    {
        public RepositoryProfile()
        {
            AddMemberConfiguration().AddMember<DataRecordMemberConfiguration>();
            CreateMap<IDataRecord, Student>();
            CreateMap<IDataRecord, Car>().ForMember(
                                            car => car.Student,
                                            match => match.MapFrom((reader, car, i, ctx) => ctx.Mapper.Map<IDataRecord, Student>(reader)));
        }
    }
}
