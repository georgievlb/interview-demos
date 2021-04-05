using Autofac;
using Backend.Persistence;
using Backend.Persistence.Interfaces;
using Backend.Service;
using Backend.Service.Interfaces;

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

            return builder.Build();
        }
        static void Main(string[] args)
        {
            CompositeRoot().Resolve<Application>().Run();
        }
    }
}
