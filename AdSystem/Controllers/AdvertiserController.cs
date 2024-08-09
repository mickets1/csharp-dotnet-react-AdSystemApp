using Microsoft.AspNetCore.Mvc;
using AdSystem.Data;
using AdSystem.Models;

namespace AdSystem.Controllers
{
    [Route("api/[controller]")]
[ApiController]
public class AdvertiserController : ControllerBase
{
    private readonly DataAccessLayer _dataAccessLayer;

    public AdvertiserController(DataAccessLayer dataAccessLayer)
    {
        _dataAccessLayer = dataAccessLayer;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Advertiser>>> GetAdvertisers()
    {
        var advertisers = await _dataAccessLayer.GetAdvertisersAsync();
        return Ok(advertisers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Advertiser>> GetAdvertiser(int id)
    {
        var advertiser = await _dataAccessLayer.GetAdvertiserByIdAsync(id);
        if (advertiser == null)
        {
            return NotFound();
        }
        return Ok(advertiser);
    }

    [HttpPost]
    public async Task<ActionResult> AddAdvertiser(Advertiser advertiser)
    {
        await _dataAccessLayer.AddAdvertiserAsync(advertiser);
        return CreatedAtAction(nameof(GetAdvertiser), new { id = advertiser.Id }, advertiser);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAdvertiser(int id, [FromBody] Advertiser advertiser)
    {
        if (id != advertiser.Id)
        {
            return BadRequest("Advertiser ID mismatch.");
        }

        var existingAdvertiser = await _dataAccessLayer.GetAdvertiserByIdAsync(id);
        if (existingAdvertiser == null)
        {
            return NotFound("Advertiser not found.");
        }

        existingAdvertiser.Name = advertiser.Name;
        existingAdvertiser.OrganizationNumber = advertiser.OrganizationNumber;
        existingAdvertiser.PhoneNumber = advertiser.PhoneNumber;
        existingAdvertiser.Address = advertiser.Address;
        existingAdvertiser.PostalCode = advertiser.PostalCode;
        existingAdvertiser.City = advertiser.City;
        existingAdvertiser.BillingAddress = advertiser.BillingAddress;
        existingAdvertiser.BillingPostalCode = advertiser.BillingPostalCode;
        existingAdvertiser.BillingCity = advertiser.BillingCity;
        existingAdvertiser.IsSubscriber = advertiser.IsSubscriber;

        await _dataAccessLayer.UpdateAdvertiserAsync(existingAdvertiser);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAdvertiser(int id)
    {
        await _dataAccessLayer.DeleteAdvertiserAsync(id);
        return NoContent();
    }
}
}