namespace Backend.Application.Countries.Interfaces
{
    public interface ICountryNameMappingService
    {
        string MapCountryNameToIso3166(string countryName);
    }
}
