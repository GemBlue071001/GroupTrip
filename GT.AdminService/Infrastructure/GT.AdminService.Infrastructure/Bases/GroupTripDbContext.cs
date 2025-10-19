

using GT.AdminService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace GT.AdminService.Infrastructure.Bases
{
    public class GroupTripDbContext : DbContext
    {
        public GroupTripDbContext(DbContextOptions<GroupTripDbContext> options) : base(options)
        {

        }
       
        public DbSet<Policy> Policies { get; set; } 

        public DbSet<PolicyType> PolicyTypes { get; set; }
    }
}
