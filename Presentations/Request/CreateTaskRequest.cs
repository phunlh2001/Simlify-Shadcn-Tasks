using TaskManagement.Persistences.Enums;
using TaskManagement.Presentations.Models;

namespace TaskManagement.Presentations.Request
{
    public class CreateTaskRequest
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public TaskEntityStatus Status { get; set; }
        public TaskPriority Priority { get; set; }
        public List<TagModel> Tags { get; set; } = [];
    }
}
