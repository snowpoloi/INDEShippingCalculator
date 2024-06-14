using Microsoft.EntityFrameworkCore;
using INDEShipping.Models;

namespace INDEShipping.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<TransportCompany> TransportCompanies { get; set; }
        public DbSet<PostalCode> PostalCodes { get; set; }
        public DbSet<Offer> Offers { get; set; }
    }
}
