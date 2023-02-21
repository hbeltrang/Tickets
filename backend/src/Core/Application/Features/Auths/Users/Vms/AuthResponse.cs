﻿namespace Tickets.Application.Features.Auths.Users.Vms
{
    public class AuthResponse
    {
        public string? Id { get; set; }

        public string? Name { get; set; }

        public string? LastName { get; set; }

        public string? Phone { get; set; }

        public string? Username { get; set; }

        public string? Email { get; set; }

        public string? Token { get; set; }

        public string? Avatar { get; set; }

        public ICollection<string>? Roles { get; set; }
        public bool IsActive { get; set; }

    }
}
