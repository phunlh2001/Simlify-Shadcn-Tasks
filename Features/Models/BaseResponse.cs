using System.Net;

namespace TaskManagement.Features.Models
{
    public class BaseResponse<T> where T : class
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
