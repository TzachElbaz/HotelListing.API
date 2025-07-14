using Microsoft.AspNetCore.Mvc;
using HotelListing.API.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelListing.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class hotelsController : ControllerBase
    {
        private static List<Hotel> hotels = new List<Hotel>
        {
            new  Hotel { Id = 1, Name = "Grand Plaza", Address = "123 Main St", Rating = 4.5 },
            new Hotel { Id = 2, Name = "Ocean View", Address = "456 Beach Rd", Rating = 4.8 }
        };

        // GET: <hotelsController>
        [HttpGet]
        public ActionResult<IEnumerable<Hotel>> Get()
        {
            return Ok(hotels);
        }

        // GET <hotelsController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var hotel = hotels.FirstOrDefault(h => h.Id == id);
            if (hotel == null) return NotFound();
            return Ok(hotel);
        }

        // POST <hotelsController>
        [HttpPost]
        public ActionResult<Hotel> Post([FromBody] Hotel newHotel)
        {
            if (hotels.Any(h => h.Id == newHotel.Id))
                return BadRequest("Hotel with this ID already exists.");
            hotels.Add(newHotel);
            return CreatedAtAction(nameof(Get), new { id = newHotel.Id }, newHotel);
        }

        // PUT <hotelsController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Hotel updatedHotel)
        {
            var existingHotel = hotels.FirstOrDefault(h => h.Id == id);
            if (existingHotel == null) return NotFound();

            existingHotel.Name = updatedHotel.Name;
            existingHotel.Address = updatedHotel.Address;
            existingHotel.Rating = updatedHotel.Rating;

            return NoContent();
        }

        // DELETE <hotelsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var hotel = hotels.FirstOrDefault(h => h.Id == id);
            if (hotel == null) return NotFound(new {message = "Hotel not found"});
            
            hotels.Remove(hotel);
            return NoContent();
        }
    }
}
