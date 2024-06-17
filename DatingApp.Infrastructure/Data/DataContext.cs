using DatingApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Infrastructure.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    // dotnet ef --startup-project ../DatingApp.Api/ migrations add Initial

    public DbSet<AppUser> Users { get; set; }
}
