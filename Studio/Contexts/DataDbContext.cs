using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Studio.Models;
using System.Reflection.Metadata.Ecma335;

namespace Studio.Contexts
{
    public class DataDbContext : IdentityDbContext
    {
        public DataDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet <Service>Services { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }

        //Update and CreatedTimer
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            IEnumerable<EntityEntry<Slider>> entries = ChangeTracker.Entries<Slider>();
            IEnumerable<EntityEntry<Service>> entriess = ChangeTracker.Entries<Service>();
            foreach (EntityEntry<Slider> entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    DateTime dateTime = DateTime.UtcNow;
                    DateTime aztime = dateTime.AddHours(4);
                    entry.Entity.CreatedAt = aztime;
                    entry.Entity.UptadedAt = null;
                }
                else if (entry.State == EntityState.Modified)
                {
                    DateTime datetime = DateTime.UtcNow;
                    DateTime aztime = datetime.AddHours(4);
                    entry.Entity.UptadedAt = aztime;
                    var modi = entry.Properties.Where(prop => prop.IsModified && !prop.Metadata.IsPrimaryKey());
                    if (!modi.Any())
                    {
                        entry.Entity.UptadedAt = null;
                    }
                }
            }
            foreach (EntityEntry<Service> entry in entriess)
            {
                if (entry.State == EntityState.Added)
                {
                    DateTime dateTime = DateTime.UtcNow;
                    DateTime azTime = dateTime.AddHours(4);
                    entry.Entity.CreatedAt = azTime;
                    entry.Entity.UptadedAt = null;
                }
                else if (entry.State == EntityState.Modified)
                {
                    DateTime datetime = DateTime.UtcNow;
                    DateTime azTime = datetime.AddHours(4);
                    entry.Entity.UptadedAt = azTime;
                    var modifi = entry.Properties.Where(prop => prop.IsModified && !prop.Metadata.IsPrimaryKey());
                    if (!modifi.Any())
                    {
                        entry.Entity.UptadedAt = null;
                    }
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }

        
}
