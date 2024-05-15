using Microsoft.EntityFrameworkCore;
using PropertyAPI.Models;

namespace PropertyAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Property> Properties => Set<Property>();
        public DbSet<Contact> Contact => Set<Contact>();
        public DbSet<PropertyOwnership> PropertyOwnership => Set<PropertyOwnership>();
        public DbSet<PropertyPriceChange> PropertyPriceChange => Set<PropertyPriceChange>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Property to Property Ownerships (one-to-many)
            /*modelBuilder.Entity<Property>()
                .HasMany(p => p.PropertyOwnerships)
                .WithOne(po => po.Property)
                .HasForeignKey(po => po.PropertyId);

            // Contact to Property Ownerships (one-to-many)
            modelBuilder.Entity<Contact>()
                .HasMany(c => c.PropertyOwnerships)
                .WithOne(po => po.Contact)
                .HasForeignKey(po => po.ContactId);

            // Property to Price Changes (one-to-many)
            modelBuilder.Entity<Property>()
                .HasMany(p => p.PriceChanges)
                .WithOne(pc => pc.Property)
                .HasForeignKey(pc => pc.PropertyId);

            /*modelBuilder.Entity<PropertyOwnership>()
                .HasOne(po => po.Property)
                .WithMany(p => p.PropertyOwnerships)
                .HasForeignKey(po => po.PropertyId);*/

            /*modelBuilder.Entity<PropertyOwnership>()
                .HasOne(po => po.Contact)
                .WithMany(c => c.PropertyOwnerships)
                .HasForeignKey(po => po.ContactId);

            // Adding indices and other configurations
            modelBuilder.Entity<Property>()
                .HasIndex(p => p.PropertyName);  // Index on PropertyName

            modelBuilder.Entity<Contact>()
                .HasIndex(c => new { c.FirstName, c.LastName });  // Index on names for quicker searches

            modelBuilder.Entity<PropertyOwnership>().ToTable("PropertyOwnership");*/

            // Define primary keys and relationships
            modelBuilder.Entity<Property>()
                .HasKey(p => p.PropertyId);

            modelBuilder.Entity<Contact>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<PropertyOwnership>()
                .HasKey(po => po.Id);

            modelBuilder.Entity<PropertyPriceChange>()
                .HasKey(pc => pc.Id);
        }
    }
}
