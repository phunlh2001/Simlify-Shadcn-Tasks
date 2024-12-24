namespace TaskManagement.Persistences.Entities
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<TaskEntity> Tasks { get; set; } = [];
        public List<TaskTag> TaskTags { get; set; } = [];
    }
}
