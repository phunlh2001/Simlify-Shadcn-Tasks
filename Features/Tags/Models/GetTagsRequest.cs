using TaskManagement.Core.Interfaces;

namespace TaskManagement.Features.Tags.Models
{
    public class GetTagsRequest : IPaginationRequest
    {
        public int Total { get; set; }
        public int Page { get; set; }
    }
}
