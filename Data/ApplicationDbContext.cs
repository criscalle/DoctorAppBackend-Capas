using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System.Reflection;


namespace Data;

public class ApplicationDbContext : IdentityDbContext<UserApplication, RolApplication, int, IdentityUserClaim<int>
                                                         , RolUserApplication, IdentityUserLogin<int>, IdentityRoleClaim<int>
                                                         , IdentityUserToken<int>>    // DbContext  antes de identity se utilizaba que heredara del dbcontext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<UserApplication> UserApplication { get; set; }

    public DbSet<User> users {  get; set; }
    public DbSet<Speciality> Specialities { get; set; }  // en la migracion a base de datos debe ser en plural (specialities / users)
    public DbSet<Medico> Medicos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }


}
