using BusinessLayer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class SportAgencyDbContext : IdentityDbContext<User>
    {
        public SportAgencyDbContext() : base()
        {

        }
        public SportAgencyDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
        .HasDiscriminator<Role>("UserRole")
        .HasValue<Athlete>(Role.Athlete)
        .HasValue<Club>(Role.Club);

            modelBuilder.Entity<Ad>()
        .HasDiscriminator<AdType>("AdType")
        .HasValue<AthleteAd>(AdType.AthleteAd)
        .HasValue<ClubAd>(AdType.ClubAd);

        }

        public DbSet<Ad> Ads { get; set; }
    }
}
