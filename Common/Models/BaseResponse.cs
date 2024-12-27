using System.Net;

namespace TaskManagement.Common.Models
{
    public class BaseResponse<T> where T : class
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public T Info { get; set; }
    }
}
