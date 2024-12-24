using System.Text.Json.Serialization;

namespace TaskManagement.Persistences.Entities
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore] public List<TaskModel> Tasks { get; set; } = [];
        [JsonIgnore] public List<TaskTag> TaskTags { get; set; } = [];
    }
}
