using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Data.Inicializador;

public class DbInicializador : IdbInicializador
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<UserApplication> _userManager;
    private readonly RoleManager<RolApplication> _roleManager;

    public DbInicializador(ApplicationDbContext context, UserManager<UserApplication> userManager, RoleManager<RolApplication> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async void Inicializar()
    {
        try
        {
            if(_context.Database.GetPendingMigrations().Count()>0)
            {
                _context.Database.Migrate(); // cuando se ejecuta por primera vez nuestra aplicacion
            }
        }
        catch(Exception)
        {
            throw;
        }

        // Datos iniciales  / crear roles

        if (_context.Roles.Any(r => r.Name == "Admin")) return;

        _roleManager.CreateAsync(new RolApplication { Name = "Admin" }).GetAwaiter().GetResult();
        _roleManager.CreateAsync(new RolApplication { Name = "Agendador" }).GetAwaiter().GetResult();
        _roleManager.CreateAsync(new RolApplication { Name = "Doctor" }).GetAwaiter().GetResult();

        // crear usuario Administrador

        var user = new UserApplication
        {
            UserName = "administrador",
            Email = "administrador@doctorapp.com",
            Apellido = "Calle",
            Nombre = "Cristina"
        };
        _userManager.CreateAsync(user, "Admin123").GetAwaiter().GetResult(); 
        // asignar el Rol de Admin para el usuario
        UserApplication userAdmin = _context.UserApplication.Where(u => u.UserName == "administrador").FirstOrDefault();
        _userManager.AddToRoleAsync(userAdmin, "Admin").GetAwaiter().GetResult();

    }
}
