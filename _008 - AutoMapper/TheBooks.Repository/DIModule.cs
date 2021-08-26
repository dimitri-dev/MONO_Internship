using Autofac;
using System.Configuration;
using System.Data.SqlClient;
using TheBooks.Repository.Common;

namespace TheBooks.Repository
{
    public class DIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CarsRepository>().As<ICarsRepository>();
            builder.RegisterType<StudentsRepository>().As<IStudentsRepository>();

            builder
                .Register(c => new SqlConnection(ConfigurationManager.ConnectionStrings["monoDB"].ConnectionString))
                .As<SqlConnection>()
                .InstancePerRequest();
        }
    }
}
