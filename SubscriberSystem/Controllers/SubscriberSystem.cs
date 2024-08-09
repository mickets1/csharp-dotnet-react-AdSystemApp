using Microsoft.AspNetCore.Mvc;
using SubscriberSystem.Data;
using SubscriberSystem.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SubscriberSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriberController : ControllerBase
    {
        private readonly DataAccessLayer _dataAccessLayer;

        public SubscriberController(DataAccessLayer dataAccessLayer)
        {
            _dataAccessLayer = dataAccessLayer;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subscriber>>> GetSubscribers()
        {
            var subscribers = await _dataAccessLayer.GetSubscribersAsync();
            return Ok(subscribers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Subscriber>> GetSubscriber(int id)
        {
            var subscriber = await _dataAccessLayer.GetSubscriberByIdAsync(id);

            if (subscriber == null)
            {
                return NotFound();
            }
            return Ok(subscriber);
        }

        [HttpPost]
        public async Task<ActionResult> AddSubscriber(Subscriber subscriber)
        {
            await _dataAccessLayer.AddSubscriberAsync(subscriber);
            return CreatedAtAction(nameof(GetSubscriber), new { id = subscriber.SubscriptionNumber }, subscriber);
        }

        [HttpPut("{subscriptionNumber}")]
        public async Task<IActionResult> UpdateSubscriber(int subscriptionNumber, [FromBody] Subscriber updatedSubscriber)
        {
            // Check if the subscription number in the URL matches the one in the request body
            if (subscriptionNumber != updatedSubscriber.SubscriptionNumber)
            {
                return BadRequest("Subscription number in the URL does not match the one in the request body.");
            }

            // Fetch the existing subscriber
            var existingSubscriber = await _dataAccessLayer.GetSubscriberByIdAsync(subscriptionNumber);
            if (existingSubscriber == null)
            {
                return NotFound(); // Return 404 if subscriber not found
            }

            // Update the existing subscriber's properties
            existingSubscriber.Name = updatedSubscriber.Name;
            existingSubscriber.Address = updatedSubscriber.Address;
            existingSubscriber.PostalCode = updatedSubscriber.PostalCode;
            existingSubscriber.City = updatedSubscriber.City;
            existingSubscriber.PhoneNumber = updatedSubscriber.PhoneNumber;

            // Save changes
            await _dataAccessLayer.UpdateSubscriberAsync(existingSubscriber);

            return NoContent(); // Return 204 No Content on successful update
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubscriber(int id)
        {
            await _dataAccessLayer.DeleteSubscriberAsync(id);
            return NoContent(); // Return 204 No Content on successful deletion
        }
    }
}
