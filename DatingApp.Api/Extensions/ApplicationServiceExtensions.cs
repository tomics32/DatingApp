using DatingApp.Application.Interfaces;
using DatingApp.Infrastructure.Data;
using DatingApp.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Api.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddAplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddControllers();
        services.AddDbContext<DataContext>(opt =>
        {
            opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
        });
        services.AddCors();
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}
