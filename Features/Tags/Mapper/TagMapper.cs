using AutoMapper;
using TaskManagement.Core.Models;
using TaskManagement.Features.Tags.Models;
using TaskManagement.Persistences.Entities;

namespace TaskManagement.Features.Tags.Mapper
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
