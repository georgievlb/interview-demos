namespace Backend.Service.Models
{
    public class CountryDto
    {
        public string Name { get; set; }

        public long Population { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as CountryDto;

            if (other == null)
            {
                return false;
            }

            return Name.ToLowerInvariant() == other.Name.ToLowerInvariant();
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
