using Autofac;
using Backend.Persistence;
using Backend.Persistence.Interfaces;
using Backend.Service;
using Backend.Service.Interfaces;
using System.Threading.Tasks;

namespace Backend
{
    class Program
    {
        private static IContainer CompositeRoot()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Application>();
            builder.RegisterType<SqliteDbManager>().As<IDbManager>();
            builder.RegisterType<ConcreteStatService>().As<IStatService>();
            builder.RegisterType<CountryService>().As<ICountryService>();
            builder.RegisterType<CountryNameMappingService>().As<ICountryNameMappingService>();

            return builder.Build();
        }
        static async Task Main(string[] args)
        {
            await CompositeRoot().Resolve<Application>().Run();
        }
    }
}
