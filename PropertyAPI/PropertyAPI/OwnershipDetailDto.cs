namespace PropertyAPI
{
    public class OwnershipDetailDto
    {
        public int Id { get; set; }
        public Guid PropertyId { get; set; }
        public Guid ContactId { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime? EffectiveTill { get; set; }
        public decimal AcquisitionPrice { get; set; }
    }
}
