using Microsoft.AspNetCore.Identity;

namespace Models.Entities;

public class UserApplication : IdentityUser<int>
{
    public string Apellido { get; set; }
    public string Nombre { get; set; }

    public ICollection<RolUserApplication> RolUsuarios { get; set; }
}
