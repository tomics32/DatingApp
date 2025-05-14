﻿using Microsoft.AspNetCore.Identity;

namespace DatingApp.Domain.Entities;

public class AppRole : IdentityRole<int>
{
    public ICollection<AppUserRole> UserRoles { get; set; } = [];
}
