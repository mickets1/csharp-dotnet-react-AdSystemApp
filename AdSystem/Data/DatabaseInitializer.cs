using AdSystem.Models;
using System;
using System.Linq;

namespace AdSystem.Data
{
    public static class AdDatabaseInitializer
    {
        public static void Initialize(AdDbContext context)
        {
            context.Database.EnsureCreated();

            // Check if any advertisers already exist
            if (context.Advertisers.Any() || context.Ads.Any())
            {
                Console.WriteLine("Database already initialized");
                return;
            }

            // Seed data for Companies
            var advertisers = new[]
            {
                new Advertiser
                {
                    Name = "Company Name 1",
                    City = "New York",
                    Address = "456 Main St",
                    PostalCode = "10001",
                    PhoneNumber = "123-456-7890",
                    BillingAddress = "123 Main St",
                    BillingPostalCode = "10001",
                    BillingCity = "New York",
                    OrganizationNumber = "1234567890",
                    IsSubscriber = false
                },
                new Advertiser
                {
                    Name = "Company Name 2",
                    City = "Los Angeles",
                    Address = "789 Main St",
                    PostalCode = "90001",
                    PhoneNumber = "987-654-3210",
                    BillingAddress = "456 Elm St",
                    BillingPostalCode = "90001",
                    BillingCity = "Los Angeles",
                    OrganizationNumber = "0987654321",
                    IsSubscriber = false
                },
                new Advertiser
                {
                    Name = "Subscriber Name 1",
                    City = "Anytown",
                    Address = "123 Main St",
                    PostalCode = "12345",
                    PhoneNumber = "123-456-7890",
                    IsSubscriber = true
                },
                new Advertiser
                {
                    Name = "Subscriber Name 2",
                    City = "Othertown",
                    Address = "124 Main St",
                    PostalCode = "67890",
                    PhoneNumber = "987-654-3210",
                    IsSubscriber = true
                }
            };

            context.Advertisers.AddRange(advertisers);
            context.SaveChanges();

            // Seed data for Ads
            var ads = new[]
            {
                new Ad
                {
                    Title = "Used Bike for Sale",
                    Content = "Selling a used bike.",
                    AdPrice = 40,
                    ItemPrice = 100,
                    IsSubscriber = false,
                    AdvertiserId = advertisers[0].Id // Company1
                },
                new Ad
                {
                    Title = "Guitar Lessons",
                    Content = "Offering guitar lessons.",
                    AdPrice = 40,
                    ItemPrice = 50,
                    IsSubscriber = false,
                    AdvertiserId = advertisers[1].Id // Company2
                },
                new Ad
                {
                    Title = "Old Books for Sale",
                    Content = "Selling a collection of old books.",
                    AdPrice = 0,
                    ItemPrice = 20,
                    IsSubscriber = true,
                    AdvertiserId = advertisers[2].Id
                },
                new Ad
                {
                    Title = "Babysitting Services",
                    Content = "Offering babysitting services.",
                    AdPrice = 0,
                    ItemPrice = 15,
                    IsSubscriber = true,
                    AdvertiserId = advertisers[3].Id
                }
            };

            context.Ads.AddRange(ads);
            context.SaveChanges();
        }
    }
}
