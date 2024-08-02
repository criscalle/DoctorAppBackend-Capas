using System.Net;

namespace Models.DTOS;

public class ApiResponse
{
    public HttpStatusCode StatusCode { get; set; } // 200, 400
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public object Result { get; set; }  // list, entity
}
