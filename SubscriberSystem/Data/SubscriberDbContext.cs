using Microsoft.EntityFrameworkCore;
using SubscriberSystem.Models;

namespace SubscriberSystem.Data
{
    public class SubscriberDbContext : DbContext
    {
        public SubscriberDbContext(DbContextOptions<SubscriberDbContext> options) : base(options)
        {
        }

        public DbSet<Subscriber> Subscribers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Subscriber>(entity =>
        {
            entity.ToTable("tbl_subscribers");
            entity.HasKey(e => e.SubscriptionNumber);
            entity.Property(e => e.SubscriptionNumber).HasColumnName("subscription_number");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.PostalCode).HasColumnName("postal_code");
            entity.Property(e => e.City).HasColumnName("city");
        });

        }
    }
}
