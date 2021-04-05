using Autofac;
using Backend.Application;
using System.Threading.Tasks;

namespace Backend
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await DependencyConfigurator.CreateInstance().Resolve<Startup>().Run();
        }
    }
}
