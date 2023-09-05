using Autofac.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;

namespace Microsoft.Extensions.Hosting
{
    public static class HostBuilderExtensions
    {
        public static async Task BuildAndRunAsync(this IHostBuilder hostBuilder)
        {
            try
            {
                var host = hostBuilder.Build();
                await host.RunAsync();
            }
            catch (Exception ex)
            {
                // This is needed for EF Migrations to work
                // https://github.com/dotnet/runtime/issues/60600
                var type = ex.GetType().Name;
                if (type.Equals("StopTheHostException", StringComparison.Ordinal))
                {
                    throw;
                }
                Console.WriteLine(ex.ToString());
                // a non-zero exit code must be returned if there's a failure
                // so that any hosting process can tell that the application has failed
                Environment.Exit(1);
            }
        }

        public static IHostBuilder RegisterDefaults(this IHostBuilder hostBuilder)
        {
            return hostBuilder.UseServiceProviderFactory(new AutofacServiceProviderFactory())
                              .UseSerilog((hostContext, serviceProvider, loggingBuilder) =>
                              {
                                  loggingBuilder
                                      .Enrich.FromLogContext()
                                      .MinimumLevel.Information()
                                      .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                                      .ReadFrom.Configuration(hostContext.Configuration)
                                      .WriteTo.Console();
                              });
        }
    }
}
