namespace API.Errores;

public class ApiException : ApiErrorResponse
{
    public ApiException(int statusCode, string message = null, string detail = null) : base(statusCode, message)
    {
        Detail = detail;
    }
    public string Detail { get; set; } // el ApiErrorResponse ya cuenta con statuscode y message por lo que ApiException hereda de ApiErrorResponse para tener statusCode, message, y anexa detail la base es : base(statusCode, message) que es lo que trae
}
