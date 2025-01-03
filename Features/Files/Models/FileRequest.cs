namespace TaskManagement.Features.Files.Models
{
    public class FileRequest
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string Data { get; set; }
    }
}
