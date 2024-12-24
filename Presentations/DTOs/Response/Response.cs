using System.Net;

namespace TaskManagement.Presentations.DTOs.Response
{
    public class Response<T> where T : class
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
