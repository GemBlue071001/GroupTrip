using Microsoft.AspNetCore.Identity;

namespace GT.AuthService.Domain.Entities
{
    public class ApplicationUserLogins : IdentityUserLogin<Guid>
    {
        public DateTimeOffset CreatedTime { get; set; }

        public DateTimeOffset LastUpdatedTime { get; set; }

        public DateTimeOffset? DeletedTime { get; set; }
        public ApplicationUserLogins()
        {
            CreatedTime = DateTime.Now;
            LastUpdatedTime = CreatedTime;
        }
    }
}
