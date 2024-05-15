namespace PropertyAPI
{
    public class Contact
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        
        // Navigation property
        public virtual ICollection<PropertyOwnership> PropertyOwnerships { get; set; }
    }
}
