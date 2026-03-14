using LinkedInJobHunter.Application.Interfaces;
using LinkedInJobHunter.Application.Models;
using LinkedInJobHunter.Infrastructure.Scrapers;
using Microsoft.AspNetCore.Mvc;

namespace LinkedInJobHunter.API.Controllers
{
    [ApiController]
    [Route("api/jobs")]
    public class JobsController : ControllerBase
    {
        private readonly IJobService _jobService;
        private readonly LinkedInScraper _scraper;

        public JobsController(IJobService jobService, LinkedInScraper scraper)
        {
            _jobService = jobService;
            _scraper = scraper;
        }

        [HttpGet]
        public async Task<IActionResult> GetJobs(
            [FromQuery] PagedRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _jobService.GetJobsAsync(request, cancellationToken);

            return Ok(result);
        }

        [HttpGet("latest")]
        public async Task<IActionResult> GetLatestJobs(CancellationToken cancellationToken)
        {
            var jobs = await _jobService.GetLatestJobsAsync(20, cancellationToken);

            return Ok(jobs);
        }

        [HttpPost("scrape")]
        public async Task<IActionResult> ScrapeJobs(CancellationToken cancellationToken)
        {
            var jobs = await _scraper.ScrapeJobsAsync();

            await _jobService.SaveScrapedJobsAsync(jobs, cancellationToken);

            return Ok(new
            {
                message = "Scraping completed",
                count = jobs.Count
            });
        }
    }
}