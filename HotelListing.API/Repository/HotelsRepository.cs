using HotelListing.API.Contracts;
using HotelListing.API.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Repository
{
    public class HotelsRepository : GenericRepository<Hotel>, IHotelsRepository 
    {
        private readonly HotelListingDbContext _context;

        public HotelsRepository(HotelListingDbContext context) :base(context)
        {
            _context = context;
        }

        public async Task<Hotel> GetDetails(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel != null)
            hotel.Country = await _context.Countries.FindAsync(hotel.CountryId);
            return hotel;
        }
    }
}
