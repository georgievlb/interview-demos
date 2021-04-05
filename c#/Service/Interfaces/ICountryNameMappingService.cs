namespace Backend.Service.Interfaces
{
    public interface ICountryNameMappingService
    {
        string MapCountryNameToIso3166(string countryName);
    }
}
