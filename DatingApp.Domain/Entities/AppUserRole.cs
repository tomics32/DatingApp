using Microsoft.AspNetCore.Identity;

namespace DatingApp.Domain.Entities;

public class AppUserRole : IdentityUserRole<int>
{
    public AppUser User { get; set; } = null!;
    public AppRole Role { get; set; } = null!;
}
