using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBooks.Repository.Common;

namespace TheBooks.Repository
{
    public class DIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CarsRepository>().As<ICarsRepository>();
            builder.RegisterType<StudentsRepository>().As<IStudentsRepository>();
        }
    }
}
