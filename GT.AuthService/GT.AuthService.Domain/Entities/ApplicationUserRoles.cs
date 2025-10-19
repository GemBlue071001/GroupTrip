﻿using Microsoft.AspNetCore.Identity;

namespace GT.AuthService.Domain.Entities
{
    public class ApplicationUserRoles : IdentityUserRole<Guid>
    {
        public DateTimeOffset CreatedTime { get; set; }

        public DateTimeOffset LastUpdatedTime { get; set; }

        public DateTimeOffset? DeletedTime { get; set; }
        public ApplicationUserRoles()
        {
            CreatedTime = DateTime.Now;
            LastUpdatedTime = CreatedTime;
        }
    }
}
