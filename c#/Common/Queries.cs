namespace Backend.Common
{
    public static class Queries
    {
        public const string GetAllCountriesWithPopulation = @"
SELECT co.CountryId, co.CountryName, CAST(SUM(ci.Population) AS INT) AS CountryPopulation
FROM Country co
    INNER JOIN State s
    ON co.CountryId = s.CountryId
        INNER JOIN City ci
        ON s.StateId = ci.StateId
GROUP BY co.CountryName
";
    }
}
