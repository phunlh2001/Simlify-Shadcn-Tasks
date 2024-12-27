using TaskManagement.Common.Interfaces;

namespace TaskManagement.Features.Tasks.Models
{
    public class GetTasksRequest : IPaginationRequest
    {
        public int Total { get; set; }
        public int Page { get; set; }
        public string SortBy { get; set; } = "name";
        public string SortOrder { get; set; } = "ASC";
    }
}
