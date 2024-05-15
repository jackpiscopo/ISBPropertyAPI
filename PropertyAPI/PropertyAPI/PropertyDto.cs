namespace PropertyAPI
{
    public class PropertyDto
    {
        public Guid PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyAddress { get; set; }
        public decimal Price { get; set; }
        public DateTime? RegistrationDate { get; set; }
    }
}
