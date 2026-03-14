using AutoMapper;
using LinkedInJobHunter.Application.DTOs;
using LinkedInJobHunter.Application.Interfaces;
using LinkedInJobHunter.Application.Models;
using LinkedInJobHunter.Domain.Entities;
using LinkedInJobHunter.Domain.Interfaces;

namespace LinkedInJobHunter.Application.Services
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository;
        private readonly IMapper _mapper;

        public JobService(IJobRepository jobRepository, IMapper mapper)
        {
            _jobRepository = jobRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<JobDto>> GetJobsAsync(
            PagedRequest request,
            CancellationToken cancellationToken)
        {
            var jobs = await _jobRepository.GetAllAsync(
                request.Page,
                request.PageSize,
                cancellationToken);

            var jobDtos = _mapper.Map<IEnumerable<JobDto>>(jobs);

            var result = new PagedResult<JobDto>
            {
                Items = jobDtos,
                Page = request.Page,
                PageSize = request.PageSize,
                TotalCount = jobDtos.Count()
            };

            return result;
        }

        public async Task<IEnumerable<JobDto>> GetLatestJobsAsync(
            int count,
            CancellationToken cancellationToken)
        {
            var jobs = await _jobRepository.GetLatestJobsAsync(count, cancellationToken);

            return _mapper.Map<IEnumerable<JobDto>>(jobs);
        }

        public async Task SaveScrapedJobsAsync(
            IEnumerable<Job> jobs,
            CancellationToken cancellationToken)
        {
            foreach (var job in jobs)
            {
                var exists = await _jobRepository.ExistsByUrlAsync(
                    job.PostUrl,
                    cancellationToken);

                if (!exists)
                {
                    await _jobRepository.AddAsync(job, cancellationToken);
                }
            }
        }
    }
}