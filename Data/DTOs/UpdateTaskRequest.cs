using TaskManagement.Data.Models;

namespace TaskManagement.Data.DTOs
{
    public class UpdateTaskRequest
    {
        public string TaskName { get; set; }
        public string Title { get; set; }
        public TaskModelStatus Status { get; set; }
        public TaskPriority Priority { get; set; }
    }
}
