namespace TaskManagement.Features.Tasks.Requests
{
    public class GetTasksRequest
    {
        public int Total { get; set; }
        public int Page { get; set; }
        public string SortBy { get; set; } = "name";
        public string SortOrder { get; set; } = "ASC";
    }
}
