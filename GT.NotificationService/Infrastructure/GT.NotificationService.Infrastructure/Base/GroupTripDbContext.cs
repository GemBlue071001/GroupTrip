

using GT.NotificationService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GT.NotificationService.Infrastructure.Base
{
    public class GroupTripDbContext : DbContext
    {
        public GroupTripDbContext(DbContextOptions<GroupTripDbContext> options) : base(options)
        {
            
    }
        public virtual DbSet<Test> Tests => Set<Test>();
    }
}
