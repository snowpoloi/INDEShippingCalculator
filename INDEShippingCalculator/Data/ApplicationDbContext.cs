using Microsoft.EntityFrameworkCore;
using INDEShipping.Models;

namespace INDEShipping.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<TransportCompany> TransportCompanies { get; set; }
        public DbSet<PostalCode> PostalCodes { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<XmlFieldMapping> XmlFieldMappings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ορισμός των σχέσεων
            modelBuilder.Entity<PostalCode>()
                .HasOne(pc => pc.TransportCompany)
                .WithMany(tc => tc.PostalCodes)
                .HasForeignKey(pc => pc.TransportCompanyId);

            // Προσθήκη τύπων για τις ιδιότητες decimal
            modelBuilder.Entity<Offer>()
                .Property(o => o.BaseCost).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Offer>()
                .Property(o => o.CubicRate).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Offer>()
                .Property(o => o.ExtraCostDifficult).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Offer>()
                .Property(o => o.ExtraCostPerKg).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Offer>()
                .Property(o => o.MaxWeight).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Offer>()
                .Property(o => o.MinCharge).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Offer>()
                .Property(o => o.MinWeight).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<TransportCompany>()
                .Property(tc => tc.MaxCubic).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<TransportCompany>()
                .Property(tc => tc.MaxHeight).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<TransportCompany>()
                .Property(tc => tc.MaxLength).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<TransportCompany>()
                .Property(tc => tc.MaxWeight).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<TransportCompany>()
                .Property(tc => tc.MaxWidth).HasColumnType("decimal(18,2)");
        }
    }
}
