using AutoMapper;
using LinkedInJobHunter.Application.DTOs;
using LinkedInJobHunter.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LinkedInJobHunter.Application.Mappings
{
    public class JobMappingProfile : Profile
    {
        public JobMappingProfile()
        {
            CreateMap<Job, JobDto>();
        }
    }
}