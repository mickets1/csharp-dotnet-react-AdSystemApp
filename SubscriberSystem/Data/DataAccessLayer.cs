using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SubscriberSystem.Models;

namespace SubscriberSystem.Data
{
    public class DataAccessLayer
    {
        private readonly SubscriberDbContext _context;

        public DataAccessLayer(SubscriberDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Subscriber>> GetSubscribersAsync()
        {
            return await _context.Subscribers.ToListAsync();
        }

        public async Task<Subscriber> GetSubscriberByIdAsync(int id)
        {
            return await _context.Subscribers.FindAsync(id);
        }

        public async Task AddSubscriberAsync(Subscriber subscriber)
        {
            await _context.Subscribers.AddAsync(subscriber);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSubscriberAsync(Subscriber subscriber)
        {
            var existingSubscriber = await _context.Subscribers
                .Where(s => s.SubscriptionNumber == subscriber.SubscriptionNumber)
                .FirstOrDefaultAsync();

            if (existingSubscriber == null)
            {
                throw new KeyNotFoundException("Subscriber not found");
            }

            // Update the existing subscriber's properties
            existingSubscriber.Name = subscriber.Name;
            existingSubscriber.Address = subscriber.Address;
            existingSubscriber.PostalCode = subscriber.PostalCode;
            existingSubscriber.City = subscriber.City;
            existingSubscriber.PhoneNumber = subscriber.PhoneNumber;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteSubscriberAsync(int id)
        {
            var subscriber = await _context.Subscribers.FindAsync(id);
            if (subscriber != null)
            {
                _context.Subscribers.Remove(subscriber);
                await _context.SaveChangesAsync();
            }
        }
    }
}
