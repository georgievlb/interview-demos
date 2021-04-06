using Autofac;
using Backend.Application;
using Backend.Application.Interfaces;
using Backend.Application.Services;
using Backend.Domain;
using Backend.Persistence;
using System.Threading.Tasks;

namespace Backend
{
    class Program
    {
        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Startup>();
            builder.RegisterType<SqliteDbManager>().As<IDbManager<Country>>();
            builder.RegisterType<ConcreteStatService>().As<IStatService>();
            builder.RegisterType<CountryService>().As<ICountryService>();
            builder.RegisterType<CountryNameMappingService>().As<ICountryNameMappingService>();

            return builder.Build();
        }
        static async Task Main(string[] args)
        {
            await BuildContainer().Resolve<Startup>().Run();
        }
    }
}
