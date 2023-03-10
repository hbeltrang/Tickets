using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Stripe;
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

            builder.Entity<Social>()
                .HasMany(p => p.SocialImages)
                .WithOne(r => r.Social)
                .HasForeignKey(r => r.SocialId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade); //si borra social se borran todas las images en cascada

            builder.Entity<Company>()
                .HasMany(p => p.CompanyImages)
                .WithOne(r => r.Company)
                .HasForeignKey(r => r.CompanyId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade); //si borra company se borran todas las images en cascada

        }

        public DbSet<Category>? Categories { get; set; }
        public DbSet<Country>? Countries { get; set; }
        public DbSet<State>? States { get; set; }
        public DbSet<City>? Cities { get; set; }
        public DbSet<Tax>? Taxes { get; set; }
        public DbSet<Term>? Terms { get; set; }
        public DbSet<PrivacyPolicy>? Privacys { get; set; }
        public DbSet<SocialImage>? SocialImages { get; set; }
        public DbSet<Social>? Socials { get; set; }
        public DbSet<CompanyImage>? CompanyImages { get; set; }
        public DbSet<Company>? Companies { get; set; }

        public DbSet<Promoter>? Promoters { get; set; }

    }
}
