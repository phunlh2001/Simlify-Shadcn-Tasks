using TaskManagement.Features.Common.Interfaces;

namespace TaskManagement.Features.Tags.Requests
{
    public class GetTagsRequest : IPaginationRequest
    {
        public int Total { get; set; }
        public int Page { get; set; }
    }
}
