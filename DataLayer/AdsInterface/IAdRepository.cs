using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.AdsInterface
{
    public interface IAdRepository<T, K>
    {
        Task CreateAdAsync(T item);
        Task DeleteAdAsync(K key);
        Task<T> ReadAdAsync(K key, bool navigationalProporties = false, bool isReadOnly = true);
        Task<ICollection<T>> ReadAllAdsAsync(bool navigationalProporties = false, bool isReadOnly = true);
        Task UpdateAdAsync(T item, bool navigationalProporties = false);
    }
}
