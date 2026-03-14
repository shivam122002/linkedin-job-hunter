using LinkedInJobHunter.Domain.Entities;

namespace LinkedInJobHunter.Domain.Interfaces
{
    public interface IJobRepository
    {
        Task AddAsync(Job job, CancellationToken cancellationToken);

        Task<IEnumerable<Job>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken);

        Task<IEnumerable<Job>> GetLatestJobsAsync(int count, CancellationToken cancellationToken);

        Task<bool> ExistsByUrlAsync(string postUrl, CancellationToken cancellationToken);
    }
}