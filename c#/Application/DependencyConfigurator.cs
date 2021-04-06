using Autofac;
using Backend.Application.Interfaces;
using Backend.Application.Services;
using Backend.Domain;
using Backend.Persistence;

namespace Backend.Application
{
    public class DependencyConfigurator
    {
        public static IContainer CreateInstance()
        {
            return BuildContainer();
        }
        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Startup>();
            builder.RegisterType<SqliteDbManager>().As<IDbManager<CountryAggregate>>();
            builder.RegisterType<ConcreteStatService>().As<IStatService>();
            builder.RegisterType<CountryService>().As<ICountryService>();
            builder.RegisterType<CountryNameMappingService>().As<ICountryNameMappingService>();

            return builder.Build();
        }
    }
}
