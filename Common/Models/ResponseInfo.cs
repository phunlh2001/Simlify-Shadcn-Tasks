namespace TaskManagement.Common.Models
{
    public class ResponseInfo<T> where T : class
    {
        public string Message { get; set; }
        public T Info { get; set; }
    }
}
