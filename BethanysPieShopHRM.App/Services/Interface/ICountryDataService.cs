using BethanysPieShopHRM.Shared.Domain;

namespace BethanysPieShopHRM.App.Services.Interface
{
    public interface ICountryDataService
    {
        Task<IEnumerable<Country>> GetAllCountries();
        Task<Country> GetCountryById(int countryId);
    }
}
