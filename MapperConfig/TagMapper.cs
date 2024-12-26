using AutoMapper;
using TaskManagement.Features.Common.Models;
using TaskManagement.Features.Tags.Responses;
using TaskManagement.Persistences.Entities;

namespace TaskManagement.MapperConfig
{
    public class TagMapper : Profile
    {
        public TagMapper()
        {
            CreateMap<Tag, TagResponse>().ReverseMap();

            CreateMap<Tag, TagPreload>();
        }
    }
}
