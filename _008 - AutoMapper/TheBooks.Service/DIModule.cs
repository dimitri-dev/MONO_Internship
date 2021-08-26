using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBooks.Service.Common;

namespace TheBooks.Service
{
    public class DIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CarsService>().As<ICarsService>();
            builder.RegisterType<StudentsService>().As<IStudentsService>();
        }
    }
}
