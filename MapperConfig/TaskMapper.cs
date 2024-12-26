using AutoMapper;
using TaskManagement.Features.Tasks.Requests;
using TaskManagement.Features.Tasks.Responses;
using TaskManagement.Persistences.Entities;

namespace TaskManagement.MapperConfig
{
    public class TaskMapper : Profile
    {
        public TaskMapper()
        {
            CreateMap<TaskEntity, TaskResponse>()
                .ReverseMap()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags));

            CreateMap<TaskEntity, UpdateTaskRequest>()
                .ReverseMap()
                .ForMember(dest => dest.Tags, opt => opt.Ignore());

            CreateMap<UpdateTaskRequest, TaskResponse>().ReverseMap();
        }
    }
}
