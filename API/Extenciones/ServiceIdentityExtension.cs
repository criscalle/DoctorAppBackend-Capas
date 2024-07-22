using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API.Extenciones;

public static class ServiceIdentityExtension
{
    public static IServiceCollection AddServiceIdentity(this IServiceCollection services, IConfiguration config)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) // buiilder.Services.AddAuthentication cuando estaba enn program
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
        return services;
    }
}
