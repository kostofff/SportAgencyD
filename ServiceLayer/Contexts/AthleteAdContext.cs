﻿using BusinessLayer.Entities;
using DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Contexts
{
    public class AthleteAdContext
    {
        private readonly AthleteAdRepository repository;

        public AthleteAdContext(AthleteAdRepository repository)
        {
            this.repository = repository;
        }

        #region Operations

        //Create method
        public async Task CreateAdAsync(AthleteAd item)
        { 
         await repository.CreateAdAsync(item);
        }

        //Read method
        public async Task<AthleteAd> ReadAdAsync(string key, bool navigationalProporties = false, bool isReadOnly = true)
        { 
         return await repository.ReadAdAsync(key, navigationalProporties, isReadOnly);
        }

        //Read all method
        public async Task<ICollection<AthleteAd>> ReadAllAdsAsync(bool navigationalProporties = false, bool isReadOnly = true)
        { 
         return await repository.ReadAllAdsAsync(navigationalProporties, isReadOnly);
        }

        //Update method
        public async Task UpdateAdAsync(AthleteAd item, bool navigationalProporties = false)
        { 
         await repository.UpdateAdAsync(item, navigationalProporties);
        }

        //Delete method
        public async Task DeleteAdAsync(string key)
        { 
         await repository.DeleteAdAsync(key);
        }
        #endregion
    }
}
