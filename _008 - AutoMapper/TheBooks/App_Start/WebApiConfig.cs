using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;

using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using Newtonsoft.Json.Serialization;
using TheBooks.Mapper;

namespace TheBooks
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            // Autofac configuration:
            // http://docs.autofac.org/en/latest/integration/webapi.html#per-controller-type-services

            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterModule<Service.DIModule>();
            builder.RegisterModule<Repository.DIModule>();

            builder.Register(c => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<Repository.RepositoryProfile>();
                cfg.AddProfile<RestProfile>();
            })).As<IConfigurationProvider>().SingleInstance();

            builder.Register(c => new AutoMapper.Mapper(c.Resolve<IConfigurationProvider>())).As<IMapper>();

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
