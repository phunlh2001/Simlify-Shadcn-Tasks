using System.Text.Json.Serialization;

namespace TaskManagement.Data.Models
{
    public class TaskModel
    {
        private TaskModelStatus _status = TaskModelStatus.BACK_LOG;
        private TaskPriority _priority = TaskPriority.LOW;

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Status
        {
            get { return _status.ToString(); }
            set
            {
                if (Enum.TryParse(value, out TaskModelStatus newValue))
                {
                    _status = newValue;
                }
            }
        }
        public string Priority
        {
            get { return _priority.ToString(); }
            set
            {
                if (Enum.TryParse(value, out TaskPriority newValue))
                {
                    _priority = newValue;
                }
            }
        }
        public List<Tag> Tags { get; set; } = [];
        [JsonIgnore] public List<TaskTag> TaskTags { get; set; } = [];
    }

    public enum TaskModelStatus
    {
        BACK_LOG, TODO, IN_PROGRESS, DONE, CANCELED
    }

    public enum TaskPriority
    {
        LOW, MEDIUM, HIGH
    }
}
