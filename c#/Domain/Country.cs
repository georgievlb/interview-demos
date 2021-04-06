namespace Backend.Domain
{
    public class Country
    {
        public string Name { get; set; }

        public long Population { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as Country;

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
