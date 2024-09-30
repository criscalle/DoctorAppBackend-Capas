using Azure;
using Data;
using Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.DTOS;
using Models.Entities;
using System.Net;
using System.Security.Cryptography;

namespace API.Controllers;

public class UserController : BaseApiController
{
    private readonly UserManager<UserApplication> _userManager;
    private readonly ITokenServices _token;
    private ApiResponse _apiResponse;
    private readonly RoleManager<RolApplication> _roleManager;

    public UserController(UserManager<UserApplication> userManager, ITokenServices token, RoleManager<RolApplication> roleManager)
    {
        _userManager = userManager;
        _token = token;
        _apiResponse = new();
        _roleManager = roleManager;
    }

    [Authorize(Policy = "AdminRol")]
    [HttpGet]  // api/usuario
    public async Task<ActionResult> GetUsuarios()
    {
        var users = await _userManager.Users.Select(u => new UserListDto()
        {
            username = u.UserName,
            apellido = u.Apellido,
            nombre = u.Nombre,
            email = u.Email,
            rol = string.Join(",", _userManager.GetRolesAsync(u).Result.ToArray())
        }).ToListAsync();
        _apiResponse.result = users;
        _apiResponse.isSuccess = true;
        _apiResponse.statusCode = HttpStatusCode.OK;
        return Ok(_apiResponse);
    }

    /*[Authorize]  // para que solo usuarios autorizados puedan obtener datos
    [HttpGet("{id}")] // api/usuario/id
    public async Task<ActionResult<User>> GetUserById(int id)  // si no es asincrona se le quita el async y es task con el await y camia el FindAsinc a solo find
    {
        var user = await _context.Users.FindAsync(id);
        return Ok(user);
    } */

    [Authorize(Policy = "AdminRol")]
    [HttpPost("Registro")]  // POST: api/user/Registro
    public async Task<ActionResult<UserDto>> Registro(RegistroDto registroDto)
    {
        if (await UserExist(registroDto.UserName)) return BadRequest("El Usuario ya se encuentra registrado"); // utiliza la funcion UserExist para verificar si el usuario existe

        var user = new UserApplication
        {
            UserName = registroDto.UserName.ToLower(),
            Email = registroDto.Email,
            Apellido = registroDto.Apellido,
            Nombre = registroDto.Nombre,
        };

        var Result = await _userManager.CreateAsync(user, registroDto.Password);
        if (!Result.Succeeded) return BadRequest(Result.Errors);

        var rolResult = await _userManager.AddToRoleAsync(user, registroDto.Rol);
        if (!rolResult.Succeeded) return BadRequest("Error al agregar el Rol del usuario");

        return new UserDto
        {
          username = user.UserName,
          token = await _token.CreateToken(user)
        };
    }

    [HttpPost("Login")]  // POST: api/user/login
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.username);
        if (user == null) return Unauthorized("Usuario no valido");

        var result = await _userManager.CheckPasswordAsync(user, loginDto.password);

        if (!result) return Unauthorized("Password no valido");

        return new UserDto 
        {
            username = user.UserName,
            token = await _token.CreateToken(user)
        };
    }

    [Authorize(Policy = "AdminRol")]
    [HttpGet("ListadoRoles")]
    public IActionResult GetRoles()
    {
        var roles = _roleManager.Roles.Select(r => new { NombreRol = r.Name }).ToList();
        _apiResponse.result = roles;
        _apiResponse.isSuccess = true;
        _apiResponse.statusCode = HttpStatusCode.OK;

        return Ok(_apiResponse);
    }
    private async Task<bool> UserExist(string username)
    {
        return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower()); // funcion que verifica si un usuario existe y devuelve un booleano / extension IdentityUser
    }

    [Authorize(Policy = "AdminRol")]
    [HttpDelete("{username}")]  // DELETE: api/user/{username}
    public async Task<IActionResult> DeleteUser(string username)
    {
        // Buscar el usuario por su nombre de usuario
        var user = await _userManager.FindByNameAsync(username.ToLower());

        // Verificar si el usuario existe
        if (user == null)
        {
            _apiResponse.isSuccess = false;
            _apiResponse.statusCode = HttpStatusCode.NotFound;
            _apiResponse.result = new List<string> { "Usuario no encontrado" };
            return NotFound(_apiResponse);
        }

        // Eliminar el usuario
        var result = await _userManager.DeleteAsync(user);

        // Verificar si la eliminación fue exitosa
        if (!result.Succeeded)
        {
            _apiResponse.isSuccess = false;
            _apiResponse.statusCode = HttpStatusCode.BadRequest;
            _apiResponse.result = result.Errors.Select(e => e.Description).ToList();
            return BadRequest(_apiResponse);
        }

        _apiResponse.isSuccess = true;
        _apiResponse.statusCode = HttpStatusCode.OK;
        _apiResponse.result = "Usuario eliminado exitosamente";
        return Ok(_apiResponse);
    }

}
