using HotelListing.API.Contracts;
using HotelListing.API.Data;

namespace HotelListing.API.Repository
{
    public class CountriesRepository : GenericRepository<Country>, ICountriesReposetory
    {
        public CountriesRepository(HotelListingDbContext context) : base(context)
        {
        }
    }
}
