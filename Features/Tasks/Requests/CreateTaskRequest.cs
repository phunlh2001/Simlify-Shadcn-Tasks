using TaskManagement.Features.Common.Models;
using TaskManagement.Persistences.Enums;

namespace TaskManagement.Features.Tasks.Requests
{
    public class CreateTaskRequest
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public TaskEntityStatus Status { get; set; }
        public TaskPriority Priority { get; set; }
        public List<TagPreload> Tags { get; set; } = [];
    }
}
