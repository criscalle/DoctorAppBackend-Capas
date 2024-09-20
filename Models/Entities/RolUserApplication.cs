using Microsoft.AspNetCore.Identity;

namespace Models.Entities;

public class RolUserApplication : IdentityUserRole<int>
{
    public UserApplication userApplication { get; set; }
    public RolApplication rolApplication { get; set; }
}
