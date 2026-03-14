using LinkedInJobHunter.Application.DTOs;
using LinkedInJobHunter.Application.Models;
using LinkedInJobHunter.Domain.Entities;

namespace LinkedInJobHunter.Application.Interfaces
{
    public interface IJobService
    {
        Task<PagedResult<JobDto>> GetJobsAsync(
            PagedRequest request,
            CancellationToken cancellationToken);

        Task<IEnumerable<JobDto>> GetLatestJobsAsync(
            int count,
            CancellationToken cancellationToken);

        Task SaveScrapedJobsAsync(
            IEnumerable<Job> jobs,
            CancellationToken cancellationToken);
    }
}