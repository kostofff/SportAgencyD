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
        public SportAgencyDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=KOSTOF31;Database=SportAgencyD;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
        .HasDiscriminator<Role>("UserRole")
        .HasValue<User>(Role.Admin)
        .HasValue<Athlete>(Role.Athlete)
        .HasValue<Club>(Role.Club);





            modelBuilder.Entity<AthletesApplication>(entity =>
            {
                entity.HasKey(a => a.ApplicationId);

                entity.Property(a => a.ApplicationId)
                      .IsRequired();

                entity.Property(a => a.Status)
                      .IsRequired();

                entity.Property(a => a.CreatedAt)
                      .IsRequired();

                entity.HasOne(a => a.Athlete)
                      .WithMany()
                      .HasForeignKey(a => a.AthleteId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(a => a.Club)
                      .WithMany()
                      .HasForeignKey(a => a.ClubId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(a => a.ClubAd)
                      .WithMany()
                      .HasForeignKey(a => a.ClubAdId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }

        public DbSet<AthleteAd> AthleteAds { get; set; }
        public DbSet<ClubAd> ClubAds { get; set; }
    }
}
