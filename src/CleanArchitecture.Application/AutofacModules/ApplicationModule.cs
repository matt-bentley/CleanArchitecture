using MediatR;
using Autofac;
using AutoMapper;
using System.Reflection;
using MediatR.NotificationPublishers;

namespace CleanArchitecture.Application.AutofacModules
{
    public sealed class ApplicationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                // this publisher causes problems with the EF Core DbContext
                // as it is not thread safe
                .Where(e => e != typeof(TaskWhenAllPublisher))
                .AsImplementedInterfaces();

            // Register the DomainEventHandler classes (they implement INotificationHandler<>) in assembly
            builder.RegisterAssemblyTypes(ThisAssembly)
                .AsClosedTypesOf(typeof(INotificationHandler<>));

            // Register the Command and Query handler classes (they implement IRequestHandler<>)
            builder.RegisterAssemblyTypes(ThisAssembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));

            // Register Automapper profiles
            var config = new MapperConfiguration(cfg => { cfg.AddMaps(ThisAssembly); });
            config.AssertConfigurationIsValid();

            builder.Register(c => config)
                .AsSelf()
                .SingleInstance();

            builder.Register(c =>
            {
                var ctx = c.Resolve<IComponentContext>();
                var mapperConfig = c.Resolve<MapperConfiguration>();
                return mapperConfig.CreateMapper(ctx.Resolve);
            }).As<IMapper>()
              .SingleInstance();
        }
    }
}
