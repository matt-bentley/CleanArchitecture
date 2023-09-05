using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Hosting
{
    public abstract class Job : BackgroundService
    {
        protected readonly ILogger<Job> Logger;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;

        protected Job(ILogger<Job> logger, IHostApplicationLifetime hostApplicationLifetime)
        {
            Logger = logger;
            _hostApplicationLifetime = hostApplicationLifetime;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                Logger.LogInformation("Starting Job: {type}", this.GetType().Name);

                await RunAsync(stoppingToken);

                Logger.LogInformation("Completed Job: {type}", this.GetType().Name);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error running job - {ex}", ex.ToString());
                Environment.ExitCode = 1;
                throw;
            }
            _hostApplicationLifetime.StopApplication();
        }

        protected abstract Task RunAsync(CancellationToken cancellationToken);
    }
}
