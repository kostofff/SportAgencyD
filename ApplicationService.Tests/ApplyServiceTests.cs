using BusinessLayer;
using DataLayer;
using BusinessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ApplicationService.Tests
{
    public class ApplyServiceTests
    {
        // This method creates an in-memory database for testing purposes.
        private SportAgencyDbContext GetInMemoryDb()
        {
            var options = new DbContextOptionsBuilder<SportAgencyDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new SportAgencyDbContext(options);
        }

        // This method sets up the database with a user and an ad for testing with Triple A system.
        [Fact]
        public async Task ApplyAsync_Should_Create_New_Application_When_Valid()
        {
            // Arrange
            var db = GetInMemoryDb();

            // Create a user
            var ad = new ClubAd
            {
                Id = "ad1",
                User = new User { Id = "club1", UserName = "Club1" },
                Title = "Test Ad",
                Sport = Sports.Football,
                SearchedPosition = Position.Forward,
                SearchedStrongFoot = LeftOrRightFoot.Left,
                MinimumAge = 18,
                MaximumAge = 25,
                Description = "Test description",
                CreatedAt = DateTime.UtcNow,
            };

            // Add the user to the database
            db.ClubAds.Add(ad);
            await db.SaveChangesAsync();

            var service = new SportAgencyDApplication.Services.ApplicationService(db);

            // Act
            var result = await service.ApplyAsync("ad1", "athlete1");

            // Assert
            Assert.True(result); 

            var application = db.AthletesApplication.FirstOrDefault();
            Assert.NotNull(application);
            Assert.Equal("athlete1", application.AthleteId);
            Assert.Equal("ad1", application.ClubAdId);
        }
    }
}
