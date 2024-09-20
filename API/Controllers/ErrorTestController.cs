using API.Errores;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;

namespace API.Controllers;

public class ErrorTestController : BaseApiController
{
    private readonly ApplicationDbContext _context;

    public ErrorTestController(ApplicationDbContext context)
    {
        _context = context;
    }
    [Authorize]
    [HttpGet("auth")]
    public ActionResult<string> GetNotAutorize()
    {
        return "No autorizado";
    }

    [HttpGet("not-found")]
    public ActionResult<User> GetNotFound()
    {
        var obj = _context.users.Find(-1);

        if (obj == null) return NotFound(new ApiErrorResponse(404));
        return obj;
    }

    [HttpGet("server-error")]
    public ActionResult<string> GetServerError()
    {
        var obj = _context.Users.Find(-1);
        var objString = obj.ToString();
        return objString;
    }

    [HttpGet("bad-request")]
    public ActionResult<string> GetBadRequest()
    {
        
        return BadRequest(new ApiErrorResponse(400));
    }
}
