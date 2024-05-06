﻿namespace PropertyAPI
{
    public class Property
    {
        public Guid PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyAddress { get; set; }
        public float Price { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
