using Microsoft.EntityFrameworkCore;
using AdSystem.Models;

namespace AdSystem.Data
{
    public class AdDbContext : DbContext
    {
        public AdDbContext(DbContextOptions<AdDbContext> options) : base(options)
        {
        }

        public DbSet<Ad> Ads { get; set; }
        public DbSet<Advertiser> Advertisers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ad>(entity =>
        {
            entity.ToTable("tbl_ads");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("ad_id");
            entity.Property(e => e.ItemPrice).HasColumnName("ad_itemprice");
            entity.Property(e => e.Content).HasColumnName("ad_content");
            entity.Property(e => e.Title).HasColumnName("ad_title");
            entity.Property(e => e.AdPrice).HasColumnName("ad_adprice");
            entity.HasOne(e => e.Advertiser)
                .WithMany(a => a.Ads)
                .HasForeignKey(e => e.AdvertiserId);
        });

        modelBuilder.Entity<Advertiser>(entity =>
        {
            entity.ToTable("tbl_advertisers");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("advertiser_id");
            entity.Property(e => e.Name).HasColumnName("advertiser_name");
            entity.Property(e => e.OrganizationNumber).HasColumnName("advertiser_organizationNumber");
            entity.Property(e => e.PhoneNumber).HasColumnName("advertiser_phonenumber");
            entity.Property(e => e.Address).HasColumnName("advertiser_address");
            entity.Property(e => e.PostalCode).HasColumnName("advertiser_postalcode");
            entity.Property(e => e.City).HasColumnName("advertiser_city");
            entity.Property(e => e.BillingAddress).HasColumnName("advertiser_billingaddress");
            entity.Property(e => e.BillingPostalCode).HasColumnName("advertiser_billingpostalcode");
            entity.Property(e => e.BillingCity).HasColumnName("advertiser_billingcity");
            entity.Property(e => e.IsSubscriber).HasColumnName("advertiser_issubscriber");
            entity.HasMany(e => e.Ads)
                .WithOne(e => e.Advertiser)
                .HasForeignKey(e => e.AdvertiserId);
        });
        }
    }
}
