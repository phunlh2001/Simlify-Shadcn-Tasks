using AutoMapper;
using TaskManagement.Features.Models;
using TaskManagement.Features.Tags.Responses;
using TaskManagement.Features.Tasks.Responses;
using TaskManagement.Persistences.Entities;

namespace TaskManagement.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<TaskEntity, TaskResponse>()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags))
                .ReverseMap();

            CreateMap<Tag, TagResponse>().ReverseMap();
            CreateMap<Tag, TagModel>();
        }
    }
}
