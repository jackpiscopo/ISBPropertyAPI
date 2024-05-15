namespace PropertyAPI.Models
{
    public class PropertyPriceChange
    {
        public int Id { get; set; }
        public Guid PropertyId { get; set; }
        public decimal NewPrice { get; set; }
        public DateTime ChangeDate { get; set; }

        // Navigation property
        public virtual Property Property { get; set; }
    }
}
