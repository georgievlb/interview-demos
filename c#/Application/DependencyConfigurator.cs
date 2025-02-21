﻿using Autofac;
using Autofac.Extras.NLog;
using Backend.Application.Countries.Interfaces;
using Backend.Application.Countries.Services;
using Backend.Domain.Countries;
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
            builder.RegisterType<CountryAggregateManager>().As<ICountryAggregateManager>();
            builder.RegisterType<ConcreteStatService>().As<IStatService>();
            builder.RegisterType<CountryService>().As<ICountryService>();
            builder.RegisterType<CountryNameMappingService>().As<ICountryNameMappingService>();
            builder.RegisterModule<NLogModule>();

            return builder.Build();
        }
    }
}
