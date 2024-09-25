using Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Models.Entities;
using System.Text;

namespace API.Extenciones;

public static class ServiceIdentityExtension
{
    public static IServiceCollection AddServiceIdentity(this IServiceCollection services, IConfiguration config)
    {
        services.AddIdentityCore<UserApplication>(opt =>
        {
            opt.Password.RequireNonAlphanumeric = false;
        })
           .AddRoles<RolApplication>()
           .AddRoleManager<RoleManager<RolApplication>>()
           .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) // builder.Services.AddAuthentication cuando estaba enn program
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey
                                               (Encoding.UTF8.GetBytes(config["TokenKey"])),  // (Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"])) asi era cuando estaba en program
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        services.AddAuthorization(opt =>  
                {
                    opt.AddPolicy("AdminRol", policy => policy.RequireRole("Admin"));
                    opt.AddPolicy("AdminAgendadorRol", policy => policy.RequireRole("Admin", "Agendador"));
                    opt.AddPolicy("AdminDoctorRol", policy => policy.RequireRole("Admin", "Doctor"));
                });

        return services;
    }
}
