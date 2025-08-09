using BusinessLayer.Entities;
using DataLayer.AdsInterface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataLayer.Repositories
{
    public class AthleteAdRepository : IAdRepository<AthleteAd, string>
    {
        private readonly SportAgencyDbContext context;

        public AthleteAdRepository(SportAgencyDbContext context)
        {
            this.context = context;
        }
        #region CRUD
        public async Task CreateAdAsync(AthleteAd item) // sadsdsdd
        {
            User userFromDb = await context.Users.FindAsync(item.UserId);
            if (userFromDb != null)
            {
                item.User = userFromDb;
            }
            context.AthleteAds.Add(item);
            await context.SaveChangesAsync();
        }
        public async Task DeleteAdAsync(string key) // Deleting ad method
        {
            try
            {
                AthleteAd athleteAdFromDb = await ReadAdAsync(key, false, false);
                if (athleteAdFromDb == null)
                {
                    throw new ArgumentException("Ad with that id does not exist!");
                }
                context.Remove(athleteAdFromDb);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<AthleteAd> ReadAdAsync(string key, bool navigationalProporties = false, bool isReadOnly = true) // Reading single ad method
        {
            try
            {
                IQueryable<AthleteAd> query = context.AthleteAds;
                if (navigationalProporties)
                {
                    query = query.Include(a => a.User);
                }

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.FirstOrDefaultAsync(a => a.Id == key);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ICollection<AthleteAd>> ReadAllAdsAsync(bool navigationalProporties = false, bool isReadOnly = true) // Reading all ads method
        {
            try
            {
                IQueryable<AthleteAd> query = context.AthleteAds;
                if (navigationalProporties)
                {
                    query = query.Include(a => a.User);
                }

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateAdAsync(AthleteAd item, bool navigationalProporties = false) // Updating ad method
        {
            AthleteAd athleteAdFromDb = await ReadAdAsync(item.Id, navigationalProporties, false);
            athleteAdFromDb.Title = item.Title;
            athleteAdFromDb.Sport = item.Sport;
            athleteAdFromDb.Position = item.Position;
            athleteAdFromDb.Country = item.Country;
            athleteAdFromDb.City = item.City;
            athleteAdFromDb.LeftOrRighFoot = item.LeftOrRighFoot;
            athleteAdFromDb.TeamsPlayed = item.TeamsPlayed;
            athleteAdFromDb.Achievements = item.Achievements;

            if (navigationalProporties)
            {
                User userFromDb = await context.Users.FindAsync(item.UserId);
                if (userFromDb != null)
                {
                    athleteAdFromDb.User = userFromDb;
                }
                else
                {
                    athleteAdFromDb.User = item.User;
                }
            }
            await context.SaveChangesAsync(); 
        }
        #endregion
    }
}
