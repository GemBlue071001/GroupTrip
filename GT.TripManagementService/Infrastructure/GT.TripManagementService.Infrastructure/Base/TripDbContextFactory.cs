
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;
//using Npgsql.EntityFrameworkCore.PostgreSQL;

//namespace GT.TripManagementService.Infrastructure.Base
//{
//    public class TripDbContextFactory : IDesignTimeDbContextFactory<TripDbContext>
//    {
//        public TripDbContext CreateDbContext(string[] args)
//        {
//            var optionsBuilder = new DbContextOptionsBuilder<TripDbContext>();

//            // Chèn connection string PostgreSQL của bạn ở đây
//            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=TripDB;Username=xhuyzdev;Password=Wenhui35@");

//            return new TripDbContext(optionsBuilder.Options);
//        }
//    }
//}
