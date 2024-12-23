namespace TaskManagement.Data.DTOs
{
    public class GetTasksRequest
    {
        public int Total { get; set; }
        public int Page { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
    }
}
