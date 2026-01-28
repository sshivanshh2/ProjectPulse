using AutoMapper;
using ProjectPulse.Application.DTOs.Project;
using ProjectPulse.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProjectPulse.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Project mappings
            CreateMap<Project, ProjectDto>()
                .ForMember(dest => dest.TaskCount,
                    opt => opt.MapFrom(src => src.Tasks.Count))
                .ForMember(dest => dest.MemberCount,
                    opt => opt.MapFrom(src => src.ProjectUsers.Count));

            CreateMap<CreateProjectRequest, Project>();
            CreateMap<UpdateProjectRequest, Project>();
        }
    }
}