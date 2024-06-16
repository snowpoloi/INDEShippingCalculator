namespace INDEShipping.Models
{
    public class TransportCompany
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal MaxLength { get; set; }
        public decimal MaxWidth { get; set; }
        public decimal MaxHeight { get; set; }
        public decimal MaxWeight { get; set; }
        public decimal MaxCubic { get; set; }
        public string OfferType { get; set; } = string.Empty;

        public ICollection<PostalCode> PostalCodes { get; set; } = new List<PostalCode>();
    }
}
