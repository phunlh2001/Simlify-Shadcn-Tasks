using TaskManagement.Features.Common.Models;
using TaskManagement.Persistences.Enums;

namespace TaskManagement.Features.Tasks.Responses
{
    public class TaskResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public TaskEntityStatus Status { get; set; }
        public TaskPriority Priority { get; set; }
        public List<TagModel> Tags { get; set; }
    }
}
