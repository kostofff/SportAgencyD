using BusinessLayer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class SportAgencyDbContext:IdentityDbContext<User>
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

            // Конфигурация за User
            modelBuilder.Entity<User>()
                .Property(u => u.UserRole)
                .HasConversion<int>(); // Съхранява enum като int в базата данни

        }


        public DbSet<Athlete> Athletes { get; set; }
        public DbSet<AthleteAd> AthleteAds { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<ClubAd> ClubAds { get; set; }
    }
}
