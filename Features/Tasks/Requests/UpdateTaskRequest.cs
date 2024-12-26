using TaskManagement.Features.Models;
using TaskManagement.Persistences.Enums;

namespace TaskManagement.Features.Tasks.Requests
{
    public class UpdateTaskRequest
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public TaskEntityStatus Status { get; set; }
        public TaskPriority Priority { get; set; }
        public List<TagModel> Tags { get; set; } = [];
    }
}
