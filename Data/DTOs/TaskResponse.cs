namespace TaskManagement.Data.DTOs
{
    public class TaskResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public IList<TagView> Tags { get; set; }
    }
}
