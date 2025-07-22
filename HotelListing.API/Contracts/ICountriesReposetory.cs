using HotelListing.API.Data;

namespace HotelListing.API.Contracts
{
    public interface ICountriesReposetory : IGenericRepository<Country>
    {
        Task<Country> GetDetails(int id);
    }
}
