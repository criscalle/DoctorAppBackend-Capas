using System.Net;

namespace Models.DTOS;

public class ApiResponse
{
    public HttpStatusCode statusCode { get; set; } // 200, 400
    public bool isSuccess { get; set; }
    public string message { get; set; }
    public object result { get; set; }  // list, entity
}
