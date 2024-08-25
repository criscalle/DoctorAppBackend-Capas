using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System.Reflection;


namespace Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users {  get; set; }
    public DbSet<Speciality> Specialities { get; set; }  // en la migracion a base de datos debe ser en plural (specialities / users)
    public DbSet<Medico> Medicos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }


}
