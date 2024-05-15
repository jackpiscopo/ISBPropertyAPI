namespace PropertyAPI
{
    public class PriceChangeDto
    {
        public Guid PropertyId { get; set; }
        public decimal NewPrice { get; set; }
        public DateTime? ChangeDate { get; set; }
    }
}
