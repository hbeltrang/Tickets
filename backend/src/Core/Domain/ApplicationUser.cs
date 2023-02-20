﻿using Microsoft.AspNetCore.Identity;

namespace Tickets.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }

        public string? LastName { get; set; }

        public string? Phone { get; set; }

        public string? AvatarUrl { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
