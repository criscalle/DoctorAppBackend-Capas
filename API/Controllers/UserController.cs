using Data;
using Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.DTOS;
using Models.Entities;
using System.Security.Cryptography;

namespace API.Controllers;

public class UserController : BaseApiController
{
    private readonly UserManager<UserApplication> _userManager;
    private readonly ITokenServices _token;

    public UserController(UserManager<UserApplication> userManager, ITokenServices token)
    {
        _userManager = userManager;
        _token = token;
    }

    [Authorize] // para que solo usuarios autorizados puedan obtener datos
    [HttpGet]
    /* public async Task<ActionResult<IEnumerable<User>>> GetUsers()  // el IEnumerable nos devuelve una lista de usuarios   es api/users
    {
        var users = await _context.users.ToListAsync();
        return Ok(users); 
    }

    [Authorize]  // para que solo usuarios autorizados puedan obtener datos
    [HttpGet("{id}")] // api/usuario/id
    public async Task<ActionResult<User>> GetUserById(int id)  // si no es asincrona se le quita el async y es task con el await y camia el FindAsinc a solo find
    {
        var user = await _context.Users.FindAsync(id);
        return Ok(user);
    } */


    [HttpPost("Registro")]  // POST: api/user/Registro
    public async Task<ActionResult<UserDto>> Registro(RegistroDto registroDto)
    {
        if (await UserExist(registroDto.UserName)) return BadRequest("El Usuario ya se encuentra registrado"); // utiliza la funcion UserExist para verificar si el usuario existe

       // using var hmac = new HMACSHA512();  // encriptar la contraseña  // al usar AspNetCore.Identity ya no es necesario
        var user = new UserApplication
        {
            UserName = registroDto.UserName.ToLower(),
           // PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registroDto.Password)),
            // PasswordSalt = hmac.Key
        };
        /*_context.users.Add(user);
        await _context.SaveChangesAsync();*/

        var result = await _userManager.CreateAsync(user, registroDto.Password);
        if (!result.Succeeded) return BadRequest(result.Errors);

        return new UserDto
        {
          username = user.UserName,
          token = _token.CreateToken(user)
        };
    }

    [HttpPost("Login")]  // POST: api/user/login
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.username);
        if (user == null) return Unauthorized("Usuario no valido");

        var result = await _userManager.CheckPasswordAsync(user, loginDto.password);

        if (!result) return Unauthorized("Password no valido");
        /* using var hmac = new HMACSHA512(user.PasswordSalt);
         var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.password));

         for (var i = 0; i < computedHash.Length; i++)
         {
             if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Password no valido"); al usar el identity ya no es necesario
         }  */
        return new UserDto 
        {
            username = user.UserName,
            token = _token.CreateToken(user)
        };
    }

    private async Task<bool> UserExist(string username)
    {
        return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower()); // funcion que verifica si un usuario existe y devuelve un booleano / extencion IdentityUser
    }

   
}
