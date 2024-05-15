namespace PropertyAPI
{
    public class Property
    {
        public Guid PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyAddress { get; set; }
        public float Price { get; set; }
        public DateTime RegistrationDate { get; set; }

        // Navigation properties
        public virtual ICollection<PropertyOwnership> PropertyOwnerships { get; set; }
        public virtual ICollection<PropertyPriceChange> PriceChanges { get; set; }
    }
}
