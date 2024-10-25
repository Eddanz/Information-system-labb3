using Information_system_labb3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Information_system_labb3.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        public override int SaveChanges()
        {
            AddNotifications();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddNotifications();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void AddNotifications()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted)
                .ToList(); // Convert to list to avoid modifying the collection during iteration

            var notifications = new List<Notification>();

            foreach (var entry in entries)
            {
                var notification = new Notification
                {
                    EventType = entry.State.ToString(),
                    Description = $"{entry.Entity.GetType().Name} was {entry.State.ToString().ToLower()}",
                    EventDate = DateTime.Now
                };
                notifications.Add(notification);
            }

            Notifications.AddRange(notifications);
        }
    }
}
