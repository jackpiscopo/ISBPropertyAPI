namespace PropertyAPI
{
    public class PropertyOwnership
    {
        public int Id { get; set; }
        public Guid ContactId { get; set; }
        public Guid PropertyId { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime EffectiveTill { get; set; }
        public decimal AcquisitionPrice { get; set; }
        
        // Navigation properties
        public virtual Contact Contact { get; set; }
        public virtual Property Property { get; set; }
    }
}
