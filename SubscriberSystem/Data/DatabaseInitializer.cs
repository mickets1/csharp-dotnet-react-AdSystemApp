using SubscriberSystem.Data;
using SubscriberSystem.Models;
using System.Linq;

namespace SubscriberSystem
{
    public static class SubscriberDatabaseInitializer
    {
        public static void Initialize(SubscriberDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Subscribers.Any())
            {
                // Database has been seeded
                return; 
            }

            var subscribers = new[]
            {
                new Subscriber
                {
                    SubscriptionNumber = 123,
                    Name = "Subscriber Name 1",
                    Address = "123 Main St",
                    PostalCode = "12345",
                    City = "Anytown",
                    PhoneNumber = "123-456-7890"
                },
                new Subscriber
                {
                    SubscriptionNumber = 124,
                    Name = "Subscriber Name 2",
                    Address = "124 Main St",
                    PostalCode = "67890",
                    City = "Othertown",
                    PhoneNumber = "987-654-3210"
                }
            };

            context.Subscribers.AddRange(subscribers);
            context.SaveChanges();
        }
    }
}
