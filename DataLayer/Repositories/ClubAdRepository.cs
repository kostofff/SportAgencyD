using BusinessLayer.Entities;
using DataLayer.AdsInterface;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.AdsInterfaces
{
    public class ClubAdRepository : IAdRepository<ClubAd, string>
    {
        private readonly SportAgencyDbContext context;
        public ClubAdRepository(SportAgencyDbContext context)
        {
            this.context = context;
        }
        #region CRUD
        public async Task CreateAdAsync(ClubAd item)
        {
            try
            {
                User userFromDb = await context.Users.FindAsync(item.UserId);
                if (userFromDb != null)
                {
                    item.User = userFromDb;
                }
                context.Add(item);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAdAsync(string key)
        {
            try
            {
                ClubAd clubAdFromDb = await ReadAdAsync(key, false, false);
                if (clubAdFromDb == null)
                {
                    throw new ArgumentException("Ad with this id does not exist!");
                }
                context.Remove(clubAdFromDb);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ClubAd> ReadAdAsync(string key, bool navigationalProporties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<ClubAd> query = context.ClubAds;
                if (navigationalProporties)
                {
                    query = query.Include(c => c.User);
                }

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }
                return await query.FirstOrDefaultAsync(c => c.Id == key);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ICollection<ClubAd>> ReadAllAdsAsync(bool navigationalProporties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<ClubAd> query = context.ClubAds;
                if (navigationalProporties)
                {
                    query = query.Include(c => c.User);
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

        public async Task UpdateAdAsync(ClubAd item, bool navigationalProporties = false)
        {
            try
            {
                ClubAd clubAdFromDb = await ReadAdAsync(item.Id, navigationalProporties, false);
                clubAdFromDb.Title = item.Title;
                clubAdFromDb.Sport = item.Sport;
                clubAdFromDb.SearchedPosition = item.SearchedPosition;
                clubAdFromDb.SearchedStrongFoot = item.SearchedStrongFoot;
                clubAdFromDb.MinimumAge = item.MinimumAge;
                clubAdFromDb.MaximumAge = item.MaximumAge;
                clubAdFromDb.Description = item.Description;

                if (navigationalProporties)
                {
                    User userFromDb = await context.Users.FindAsync(item.UserId);
                    if (userFromDb != null)
                    {
                        clubAdFromDb.User = userFromDb;
                    }
                    else
                    {
                        clubAdFromDb.User = item.User;
                    }
                }
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }
}
