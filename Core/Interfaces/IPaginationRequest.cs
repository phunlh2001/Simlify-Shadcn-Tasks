namespace TaskManagement.Core.Interfaces
{
    public interface IPaginationRequest
    {
        public int Total { get; set; }
        public int Page { get; set; }
    }
}
