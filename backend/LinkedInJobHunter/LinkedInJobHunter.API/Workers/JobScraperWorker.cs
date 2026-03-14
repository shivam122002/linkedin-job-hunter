using LinkedInJobHunter.Application.Interfaces;
using LinkedInJobHunter.Infrastructure.Scrapers;

namespace LinkedInJobHunter.API.Workers
{
    public class JobScraperWorker : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<JobScraperWorker> _logger;

        public JobScraperWorker(
            IServiceScopeFactory scopeFactory,
            ILogger<JobScraperWorker> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Job scraping started");

                using var scope = _scopeFactory.CreateScope();

                var scraper = scope.ServiceProvider.GetRequiredService<LinkedInScraper>();
                var jobService = scope.ServiceProvider.GetRequiredService<IJobService>();

                var jobs = await scraper.ScrapeJobsAsync();

                await jobService.SaveScrapedJobsAsync(jobs, stoppingToken);

                _logger.LogInformation("Scraped {Count} jobs", jobs.Count);

                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
        }
    }
}