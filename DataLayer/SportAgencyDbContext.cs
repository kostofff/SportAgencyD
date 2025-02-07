using BusinessLayer;
using BusinessLayer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class SportAgencyDbContext : IdentityDbContext<User>
    {
        public SportAgencyDbContext() : base()
        {

        }
        public SportAgencyDbContext(DbContextOptions<SportAgencyDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
        .HasDiscriminator<Role>("UserRole")
        .HasValue<User>(Role.GeneralUser)
        .HasValue<Athlete>(Role.Athlete)
        .HasValue<Club>(Role.Club);   

        }

        public DbSet<AthleteAd> AthleteAds { get; set; }
        public DbSet<ClubAd> ClubAds { get; set; }
    }
}
