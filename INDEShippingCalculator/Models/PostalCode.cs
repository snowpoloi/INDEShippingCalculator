namespace INDEShipping.Models
{
    public class PostalCode
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Nomos { get; set; }
        public string? City { get; set; }
        public string? Area { get; set; }
        public bool IsDifficultAccess { get; set; }
        public bool NoCOD { get; set; }

        public int TransportCompanyId { get; set; }  // Foreign Key
        public TransportCompany TransportCompany { get; set; } = null!;
    }
}
