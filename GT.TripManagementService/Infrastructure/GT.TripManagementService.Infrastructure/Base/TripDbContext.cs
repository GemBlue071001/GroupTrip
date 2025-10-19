
using GT.TripManagementService.Domain.Base;
using GT.TripManagementService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
namespace GT.TripManagementService.Infrastructure.Base
{
  public class TripDbContext : DbContext
  {
    public TripDbContext(DbContextOptions<TripDbContext> options) : base(options)
    {
    }

    public virtual DbSet<Trip> Trips => Set<Trip>();
    //public virtual DbSet<TripMember> TripMembers => Set<TripMember>();
    //public virtual DbSet<TripScheduleItem> TripScheduleItems => Set<TripScheduleItem>();
    public virtual DbSet<TripTag> TripTags => Set<TripTag>();
    public virtual DbSet<TripTagRelation> TripTagRelations => Set<TripTagRelation>();
    //public virtual DbSet<Service> Services => Set<Service>();
    public virtual DbSet<TripDate> TripDates => Set<TripDate>();
    public virtual DbSet<TripDateActivity> TripDateActivities => Set<TripDateActivity>();
   // public virtual DbSet<TripService> TripServices  => Set<TripService>();
    public virtual DbSet<TripCostRange> TripCostRanges => Set<TripCostRange>();
    public virtual DbSet<TripRules> TripRules => Set<TripRules>();
    public virtual DbSet<TripDepartures> TripDepartures => Set<TripDepartures>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TripDepartures>()
                .Property(td => td.DepartureStatus)
                .HasConversion<string>();
            modelBuilder.Entity<TripRules>()
                .Property(td => td.ExperienceLevel)
                .HasConversion<string>();
            modelBuilder.Entity<Trip>()
                .Property(td => td.Status)
                .HasConversion<string>();
            // Apply all configurations in this assembly
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(TripDbContext).Assembly);
            // Trip (1) - (n) TripDate
            modelBuilder.Entity<Trip>()
                .HasMany(t => t.TripDates)
                .WithOne(td => td.Trip)
                .HasForeignKey(td => td.TripId)
                .OnDelete(DeleteBehavior.Cascade);

            // Trip (1) - (n) TripCostRange
            modelBuilder.Entity<Trip>()
                .HasMany(t => t.TripCostRanges)
                .WithOne(td => td.Trip)
                .HasForeignKey(tcr => tcr.TripId)
                .OnDelete(DeleteBehavior.Cascade);
            //Departure
            modelBuilder.Entity<Trip>()
               .HasMany(t => t.TripDepartures)
               .WithOne(td => td.Trip)
               .HasForeignKey(tcr => tcr.TripId)
               .OnDelete(DeleteBehavior.Cascade);
            //Trip Rule
            modelBuilder.Entity<Trip>()
                 .HasOne(t => t.TripRules)
                 .WithOne(d => d.Trip)
                 .HasForeignKey<TripRules>(d => d.TripId)
                 .OnDelete(DeleteBehavior.Cascade);
            //Trip Clone
            modelBuilder.Entity<Trip>()
              .HasOne(t => t.BaseTrip)
              .WithMany()  
              .HasForeignKey(t => t.BaseTripId)
              .OnDelete(DeleteBehavior.Restrict);
            // TripDate (1) - (n) DateDestination
            modelBuilder.Entity<TripDate>()
                .HasMany(td => td.DateDestinations)
                .WithOne(dd => dd.TripDate)
                .HasForeignKey(dd => dd.TripDateId)
                .OnDelete(DeleteBehavior.Cascade);

            // TripDate (1) - (n) TripService
            //modelBuilder.Entity<TripDate>()
            //    .HasMany(td => td.TripServices)
            //    .WithOne(ts => ts.TripDate)
            //    .HasForeignKey(ts => ts.TripDateId)
            //    .OnDelete(DeleteBehavior.Cascade);

            // DateDestination (1) - (n) TripDateActivity
            modelBuilder.Entity<DateDestination>()
                .HasMany(dd => dd.TripDateActivities)
                .WithOne(a => a.DateDestination)
                .HasForeignKey(a => a.DestinationId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Trip>()
               .HasMany(t => t.TripCostRanges)
               .WithOne(tcr => tcr.Trip)
               .HasForeignKey(tcr => tcr.TripId)
               .OnDelete(DeleteBehavior.Cascade);
            // Trip (1) - (n) TripTagRelation
            modelBuilder.Entity<Trip>()
                .HasMany(t => t.TripTagRelations)
                .WithOne(r => r.Trip)
                .HasForeignKey(r => r.TripId)
                .OnDelete(DeleteBehavior.Cascade);

            // TripTag (1) - (n) TripTagRelation
            modelBuilder.Entity<TripTag>()
                .HasMany(tag => tag.TagRelations)
                .WithOne(rel => rel.Tag)
                .HasForeignKey(rel => rel.TagId)
                .OnDelete(DeleteBehavior.Cascade);
    }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    if (entry.Entity.CreatedTime.HasValue)
                        entry.Entity.CreatedTime = entry.Entity.CreatedTime.Value.ToUniversalTime();
                    if (entry.Entity.LastUpdatedTime.HasValue)
                        entry.Entity.LastUpdatedTime = entry.Entity.LastUpdatedTime.Value.ToUniversalTime();
                    if (entry.Entity.DeletedTime.HasValue)
                        entry.Entity.DeletedTime = entry.Entity.DeletedTime.Value.ToUniversalTime();
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
