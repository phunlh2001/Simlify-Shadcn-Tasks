using TaskManagement.Data.Models;

namespace TaskManagement.Data.DTOs
{
    public class CreateTaskRequest
    {
        public string TaskName { get; set; }
        public string Title { get; set; }
        public TaskModelStatus Status { get; set; }
        public TaskPriority Priority { get; set; }
        public IList<TagView> Tags { get; set; }
    }

    public class TagView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
