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
                optionsBuilder.UseSqlServer("Server=tcp:sportagencyserver.database.windows.net,1433;Initial Catalog=SportAgencyDB;Persist Security Info=False;User ID=mitko;Password=31033103121212mM!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
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

            modelBuilder.Entity<ClubsApplication>(entity =>
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

                entity.HasOne(a => a.AthleteAd)
                      .WithMany()
                      .HasForeignKey(a => a.AthleteAdId)
                      .OnDelete(DeleteBehavior.Restrict);


                // За ClubsApplication - вече ти го показах
                modelBuilder.Entity<ClubsApplication>()
                    .HasOne(c => c.Club)
                    .WithMany()
                    .HasForeignKey(c => c.ClubId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Ново: За AthletesApplication
                modelBuilder.Entity<AthletesApplication>()
                    .HasOne(a => a.Athlete)
                    .WithMany()
                    .HasForeignKey(a => a.AthleteId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }

        public DbSet<AthleteAd> AthleteAds { get; set; }
        public DbSet<ClubAd> ClubAds { get; set; }
        public DbSet<AthletesApplication> AthletesApplication { get; set; }

        public DbSet<ClubsApplication> ClubsApplication { get; set; }
    }
}
