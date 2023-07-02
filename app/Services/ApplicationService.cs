using Microsoft.EntityFrameworkCore;

namespace sunstealer.mvc.Services
{
    // ajm: interface
    public interface IApplication
    {
        Models.Configuration Configuration { get; }

        bool Reconfigure(Models.Configuration configuration);
    }

    // ajm: service
    public class Application : IApplication, IHostedService
    {
        private readonly Microsoft.EntityFrameworkCore.IDbContextFactory<ApplicationDbContext> dbContextFactory;
        private readonly Microsoft.Extensions.Logging.ILogger<Application> logger;

        public Models.Configuration Configuration { get; private set; }

        public Application(Microsoft.Extensions.Logging.ILogger<Application> logger, Microsoft.EntityFrameworkCore.IDbContextFactory<ApplicationDbContext> dbContext) {
            this.Configuration = new Models.Configuration();
            this.dbContextFactory = dbContext;
            this.logger = logger;    
        }

        public bool Reconfigure(Models.Configuration configuration)
        {
            try {
                if (configuration.Validate(configuration)) {
                    return true;
                }
            } catch(System.Exception e) {
                this.logger.LogCritical(e.ToString());
            }

            return false;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("ApplicationService.StopAsync()");

            var dbContext = dbContextFactory.CreateDbContext();
            var table1 = dbContext.table1.FromSql($"select * from [dbo].[table1]").ToList();
            logger.LogInformation($"table1.rows: {table1.Count}");
            dbContext.Dispose();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("ApplicationService.StopAsync()");
            return Task.CompletedTask;
        }
    }
}