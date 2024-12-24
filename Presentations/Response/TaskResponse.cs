using TaskManagement.Persistences.Entities;
using TaskManagement.Persistences.Enums;
using TaskManagement.Presentations.Models;

namespace TaskManagement.Presentations.Response
{
    public class TaskResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public TaskEntityStatus Status { get; set; }
        public TaskPriority Priority { get; set; }
        public IList<TagModel> Tags { get; set; }

        public static List<TaskResponse> MapListFrom(List<TaskEntity> entity)
        {
            return entity.Select(t => new TaskResponse
            {
                Id = t.Id,
                Title = t.Title,
                Name = t.Name,
                Priority = t.Priority,
                Status = t.Status,
                Tags = t.Tags.Select(tag => new TagModel
                {
                    Id = tag.Id,
                    Name = tag.Name
                }).ToList()
            }).ToList();
        }

        public static TaskResponse MapFrom(TaskEntity t)
        {
            return new TaskResponse
            {
                Id = t.Id,
                Title = t.Title,
                Name = t.Name,
                Priority = t.Priority,
                Status = t.Status,
                Tags = t.Tags.Select(tag => new TagModel
                {
                    Id = tag.Id,
                    Name = tag.Name
                }).ToList()
            };
        }
    }
}
