using Microsoft.AspNetCore.Identity;

namespace GT.AuthService.Domain.Entities
{
    public class ApplicationUserTokens : IdentityUserToken<Guid>
    {
        public DateTimeOffset CreatedTime { get; set; }

        public DateTimeOffset LastUpdatedTime { get; set; }

        public DateTimeOffset? DeletedTime { get; set; }
        public ApplicationUserTokens()
        {
            CreatedTime = DateTime.Now;
            LastUpdatedTime = CreatedTime;
        }
    }
}
