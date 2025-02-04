using DatingApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Infrastructure.Data
{
    public class DataContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<AppUser> Users { get; set; }
    }
}


//dotnet ef migrations add InitialCreate --project DatingApp.Infrastructure --startup-project DatingApp.Api
//dotnet ef database update --project DatingApp.Infrastructure --startup-project DatingApp.Api