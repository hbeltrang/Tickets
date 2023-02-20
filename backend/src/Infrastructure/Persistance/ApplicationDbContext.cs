using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Tickets.Domain;
using Tickets.Domain.Common;

namespace Tickets.Infrastructure.Persistance
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var userName = "system";

            foreach (var entry in ChangeTracker.Entries<BaseDomainModel>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = userName;
                        entry.Entity.Status = Status.Active;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedBy = userName;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().Property(x => x.Id).HasMaxLength(36); //250
            builder.Entity<ApplicationUser>().Property(x => x.NormalizedUserName).HasMaxLength(90);
            builder.Entity<IdentityRole>().Property(x => x.Id).HasMaxLength(36);
            builder.Entity<IdentityRole>().Property(x => x.NormalizedName).HasMaxLength(90);

        }

        public DbSet<Category>? Categories { get; set; }
        public DbSet<Country>? Countries { get; set; }
    }
}
