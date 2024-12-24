using AutoMapper;
using TaskManagement.Persistences.Entities;
using TaskManagement.Presentations.Response;

namespace TaskManagement.Configuration
{
    public class MapperConfig
    {
        public static Mapper InititalizeAutomapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TaskEntity, TaskResponse>().ReverseMap();
                cfg.CreateMap<Tag, TagResponse>().ReverseMap();
            });

            var mapper = new Mapper(config);
            return mapper;
        }
    }
}
