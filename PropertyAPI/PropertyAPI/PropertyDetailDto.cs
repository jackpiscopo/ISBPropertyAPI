namespace PropertyAPI
{
    public class PropertyDetailDto
    {
        public Guid PropertyId { get; set; }
        public string PropertyName { get; set; }
        public decimal AskingPrice { get; set; }
        public string Owner { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public decimal? SoldPrice { get; set; }
        public List<OwnershipDetailDto> Ownerships { get; set; }
        public List<PriceChangeDto> PriceChanges { get; set; }
    }
}
