using Microsoft.AspNetCore.Identity;

namespace Models.Entities;

public class RolApplication: IdentityRole<int>
{
    public ICollection<RolUserApplication> RolUsuario { get; set; }
}
