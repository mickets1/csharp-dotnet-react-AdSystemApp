using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AdSystem.Models;

namespace AdSystem.Data
{
    public class DataAccessLayer
    {
        private readonly AdDbContext _context;

        public DataAccessLayer(AdDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Advertiser>> GetAdvertisersAsync()
        {
            // Only get company advertisers.
            // Subscriber system is responsible for getting subscriber information.
            return await _context.Advertisers
            .Where(a => !a.IsSubscriber) // Filter out subscribers
            .ToListAsync();
        }

        public async Task<Advertiser> GetAdvertiserByIdAsync(int id)
        {
            return await _context.Advertisers.FindAsync(id);
        }

        public async Task AddAdvertiserAsync(Advertiser advertiser)
        {
            await _context.Advertisers.AddAsync(advertiser);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAdvertiserAsync(Advertiser advertiser)
        {
            _context.Entry(advertiser).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAdvertiserAsync(int id)
        {
            var advertiser = await _context.Advertisers.FindAsync(id);
            if (advertiser != null)
            {
                _context.Advertisers.Remove(advertiser);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAdAsync(int id)
        {
            var ad = await _context.Ads.FindAsync(id);
            if (ad != null)
            {
                _context.Ads.Remove(ad);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAdAsync(Ad ad)
        {
            _context.Entry(ad).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
