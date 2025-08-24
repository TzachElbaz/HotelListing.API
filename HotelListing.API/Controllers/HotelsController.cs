using AutoMapper;
using HotelListing.API.Contracts;
using HotelListing.API.Data;
using HotelListing.API.Models;
using HotelListing.API.Models.Hotel;
using HotelListing.API.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;


namespace HotelListing.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class hotelsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHotelsRepository _hotelsRepository;

        public hotelsController(IMapper mapper, IHotelsRepository hotelsrepository)
        {
            _mapper = mapper;
            _hotelsRepository = hotelsrepository;
        }

        // GET: /Hotels/GetAll
        [HttpGet("GetAll")]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<HotelDto>>> GetHotels()
        {
            var hotels = await _hotelsRepository.GetAllAsync();
            return Ok(_mapper.Map<List<HotelDto>>(hotels));
        }

        // GET: Hotels?StartIndex=0&PageSize=25&PageNumber=1
        [HttpGet]
        public async Task<ActionResult<PagedResult<HotelDto>>> GetPagedHotels([FromQuery]QueryParameters queryParameters)
        {
            var pagedHotelsResult = await _hotelsRepository.GetAllAsync< HotelDto>(queryParameters);
            return Ok(pagedHotelsResult);
        }

        // GET <hotelsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelDto>> GetHotel(int id)
        {
            var hotel = await _hotelsRepository.GetAsync(id);
            if (hotel == null)
                return NotFound();

            return Ok(_mapper.Map<HotelDto>(hotel));
        }

        // POST <hotelsController>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Hotel>> PostHotel(CreateHotelDto newHotel)
        {
            var hotel = _mapper.Map<Hotel>(newHotel);
            var createdHotel = await _hotelsRepository.AddAsync(hotel);

            return CreatedAtAction("GetHotel", new { id = createdHotel.Id }, createdHotel);
        }

        // PUT <hotelsController>/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> PutHotel(int id, HotelDto updatedHotel)
        {
            if (id != updatedHotel.Id)
                return BadRequest("id Path-Parameter does not match the id field");

            var hotel = await _hotelsRepository.GetAsync(id);
            if (hotel == null) return NotFound();

            _mapper.Map(updatedHotel, hotel);
            try
            {
                await _hotelsRepository.UpdateAsync(hotel);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await HotelExists(id))
                    return NotFound();

                return Conflict(new
                {
                    ErrorCode = "ConcurrencyFailure",
                    Message = "The update operation could not be completed due to a concurrency conflict. Please refresh and try again."
                });
            }

            return NoContent();
        }

        // DELETE <hotelsController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> DeleteHotel(int id)
        {
            var hotel = await _hotelsRepository.GetAsync(id);
            if (hotel == null)
                return NotFound(new { message = "Hotel not found" });

            await _hotelsRepository.DeleteAsync(id);
            return NoContent();
        }

        private async Task<bool> HotelExists(int id)
        {
            return await _hotelsRepository.Exists(id);
        }
    }
}
