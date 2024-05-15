namespace PropertyAPI
{
    public class CombinedPropertyDetailDto
    {
        public int Id { get; set; }
        public Guid PropertyId { get; set; }
        public string PropertyName { get; set; }
        public decimal AskingPrice { get; set; }
        public string Owner { get; set; }
        public DateTime? DateOfPurchase { get; set; }
        public decimal? SoldPriceInEur { get; set; }
        public decimal? SoldPriceInUsd { get; set; }
        //public List<OwnershipDetailDto> Ownerships { get; set; }
        //public List<PriceChangeDto> PriceChanges { get; set; }
    }
}
