using Autofac;
using CleanArchitecture.Application.AutofacModules;
using CleanArchitecture.Application.Weather.IntegrationEvents;
using CleanArchitecture.Hosting;
using CleanArchitecture.Infrastructure.AutofacModules;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var hostBuilder = Worker.CreateBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMiniTransit((_, configure) =>
                    {
                        configure.UseRetry(retry =>
                        {
                            retry.Exponential(3, TimeSpan.FromSeconds(1));
                        });
                        configure.UseRabbitMQ(options =>
                        {
                            hostContext.Configuration.GetSection("EventBus").Bind(options);
                        });
                        configure.AddConsumer<WeatherForecastCreatedEventHandler>();
                    });
                })
                .ConfigureContainer<ContainerBuilder>((hostContext, container) =>
                {
                    container.RegisterModule(new InfrastructureModule(hostContext.Configuration));
                    container.RegisterModule(new ApplicationModule());
                });

await hostBuilder.BuildAndRunAsync();