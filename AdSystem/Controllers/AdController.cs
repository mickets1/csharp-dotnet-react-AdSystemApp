using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdSystem.Data;
using AdSystem.Models;
using AdSystem.Services;
using AdSystem.DTO; // Ensure this is the correct namespace for your DTOs
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdsController : ControllerBase
    {
        private readonly AdDbContext _context;
        private readonly ApiService _apiService;

        public AdsController(AdDbContext context, ApiService apiService)
        {
            _context = context;
            _apiService = apiService;
        }

        // POST: api/ads/subscriber
        [HttpPost("subscriber")]
        public async Task<IActionResult> PostSubscriberAd([FromBody] SubscriberAdRequest request)
        {
            if (request.SubscriptionNumber <= 0)
            {
                return BadRequest("Invalid subscription number.");
            }

            // Fetch subscriber information from subscriber system.
            var subscriber = await _apiService.GetSubscriberAsync(request.SubscriptionNumber);

            if (subscriber == null)
            {
                return NotFound("Subscriber not found.");
            }

            // Create or find the subscriber as an Advertiser
            var advertiser = new Advertiser
            {
                Name = subscriber.Name,
                Address = subscriber.Address,
                PostalCode = subscriber.PostalCode,
                City = subscriber.City,
                PhoneNumber = subscriber.PhoneNumber,
                BillingAddress = subscriber.Address,
                BillingPostalCode = subscriber.PostalCode,
                BillingCity = subscriber.City,
                IsSubscriber = true
            };

            // Check if advertiser already exists in the database
            var existingAdvertiser = await _context.Advertisers
                .FirstOrDefaultAsync(a => a.PhoneNumber == advertiser.PhoneNumber && a.IsSubscriber);

            if (existingAdvertiser != null)
            {
                advertiser = existingAdvertiser;
            }
            else
            {
                _context.Advertisers.Add(advertiser);
                await _context.SaveChangesAsync();
            }

            var ad = new Ad
            {
                Title = request.Title,
                Content = request.Content,
                ItemPrice = request.ItemPrice,
                AdPrice = 0, // Free for subscribers
                IsSubscriber = true,
                AdvertiserId = advertiser.Id
            };

            _context.Ads.Add(ad);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAd), new { id = ad.Id }, ad);
        }

        // POST: api/ads/company
        [HttpPost("company")]
        public async Task<IActionResult> PostCompanyAd([FromBody] CompanyAdRequest request)
        {
            if (string.IsNullOrEmpty(request.OrganizationNumber))
            {
                return BadRequest("Organization number is required.");
            }

            // Find company advertiser based on organization number
            var advertiser = await _context.Advertisers
                .FirstOrDefaultAsync(a => a.OrganizationNumber == request.OrganizationNumber);

            if (advertiser == null)
            {
                advertiser = new Advertiser
                {
                    Name = request.Name,
                    Address = request.Address,
                    PostalCode = request.PostalCode,
                    City = request.City,
                    PhoneNumber = request.PhoneNumber,
                    BillingAddress = request.BillingAddress,
                    BillingPostalCode = request.BillingPostalCode,
                    BillingCity = request.BillingCity,
                    OrganizationNumber = request.OrganizationNumber,
                    IsSubscriber = false
                };

                _context.Advertisers.Add(advertiser);
                await _context.SaveChangesAsync();
            }

            var ad = new Ad
            {
                Title = request.Title,
                Content = request.Content,
                ItemPrice = request.ItemPrice,
                AdPrice = 40, // Default price for companies
                IsSubscriber = false,
                AdvertiserId = advertiser.Id
            };

            _context.Ads.Add(ad);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAd), new { id = ad.Id }, ad);
        }

        [HttpGet("ads")]
        public async Task<ActionResult<IEnumerable<object>>> GetAds()
        {
            var ads = await _context.Ads
                .Include(a => a.Advertiser)
                .Select(a => new
                {
                    a.Id,
                    a.Title,
                    a.Content,
                    a.ItemPrice,
                    a.AdPrice,
                    AdvertiserName = a.Advertiser.Name // include advertiser name to display "Posted By"
                })
                .ToListAsync();

            return Ok(ads);
        }

        // GET: api/Ads/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ad>> GetAd(int id)
        {
            var ad = await _context.Ads.FindAsync(id);

            if (ad == null)
            {
                return NotFound();
            }

            return ad;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAd(int id, [FromBody] Ad ad)
        {
            if (id != ad.Id)
            {
                return BadRequest("Ad ID mismatch.");
            }

            var existingAd = await _context.Ads.FindAsync(id);
            if (existingAd == null)
            {
                return NotFound("Ad not found.");
            }

            existingAd.Title = ad.Title;
            existingAd.Content = ad.Content;
            existingAd.ItemPrice = ad.ItemPrice;
            existingAd.AdPrice = ad.AdPrice;
            existingAd.IsSubscriber = ad.IsSubscriber;

            _context.Ads.Update(existingAd);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Ads/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAd(int id)
        {
            var ad = await _context.Ads.FindAsync(id);
            if (ad == null)
            {
                return NotFound();
            }

            _context.Ads.Remove(ad);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdExists(int id)
        {
            return _context.Ads.Any(e => e.Id == id);
        }
    }
}
