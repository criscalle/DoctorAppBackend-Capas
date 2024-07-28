namespace API.Errores;

public class ApiErrorResponse
{
    public ApiErrorResponse(int statusCode, string message =null)
    {
        StatusCode = statusCode;
        Message = message ?? GetMessageStatusCode(statusCode);
    }

    public int StatusCode { get; set; }
    public string Message { get; set; }

    private string GetMessageStatusCode(int statusCode)
    {
        return statusCode switch
        {
            400 => "Se ha realizado una Solicitud no válida",
            401 => "No estas autorizado para este recurso",
            404 => "Recurso no encontrado",
            500 => "Error interno del Servidor",
            _ => null
        };   // el  _ es el default
    }
}
