using BusinessLayer.Entities;
using DataLayer.AdsInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Contexts
{
    public class ClubAdContext
    {
        private readonly ClubAdRepository repository;

        public ClubAdContext(ClubAdRepository repository)
        {
            this.repository = repository;
        }

        #region Operations
        public async Task CreateAdAsync(ClubAd item)
        { 
         await repository.CreateAdAsync(item);
        }

        public async Task<ClubAd> ReadAdAsync(string key, bool navigationalProporties = false, bool isReadOnly = true)
        { 
         return await repository.ReadAdAsync(key, navigationalProporties, isReadOnly);
        }

        public async Task<ICollection<ClubAd>> ReadAllAdsAsync(bool navigationalProporties = false, bool isReadOnly = true)
        { 
         return await repository.ReadAllAdsAsync(navigationalProporties, isReadOnly);
        }

        public async Task UpdateAdAsync(ClubAd item, bool navigationalProporties = false)
        { 
         await repository.UpdateAdAsync(item, navigationalProporties);
        }

        public async Task DeleteAdAsync(string key)
        { 
         await repository.DeleteAdAsync(key);
        }

        #endregion
    }
}
