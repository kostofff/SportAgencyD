using BusinessLayer.Entities;
using DataLayer;
using Microsoft.EntityFrameworkCore;

namespace SportAgencyDApplication.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly SportAgencyDbContext _context;

        public ApplicationService(SportAgencyDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ApplyAsync(string adId, string athleteId)
        {
            var ad = await _context.ClubAds.FirstOrDefaultAsync(a => a.Id == adId);
            if (ad == null) return false;

            var existingApplication = await _context.AthletesApplication
                .FirstOrDefaultAsync(a => a.AthleteId == athleteId && a.ClubAdId == ad.Id);

            if (existingApplication != null) return false;

            var application = new AthletesApplication
            {
                Status = ApplicationStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                ClubId = ad.UserId,
                ClubAdId = ad.Id,
                AthleteId = athleteId
            };

            _context.AthletesApplication.Add(application);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
