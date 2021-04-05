using Backend.Persistence.Interfaces;
using Backend.Service.Interfaces;
using System;
using System.Data.Common;

namespace Backend
{
    public class Application
    {
        private readonly IDbManager dbManager;
        private readonly IStatService statService;

        public Application(IDbManager dbManager, IStatService statService)
        {
            this.dbManager = dbManager;
            this.statService = statService;
        }

        public void Run()
        {
            Console.WriteLine("Started");
            Console.WriteLine("Getting DB Connection...");

            DbConnection conn = dbManager.getConnection();

            if (conn == null)
            {
                Console.WriteLine("Failed to get connection");
            }
        }
    }
}
