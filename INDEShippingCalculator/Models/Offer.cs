namespace INDEShipping.Models
{
    public class Offer
    {
        public int Id { get; set; }
        public int TransportCompanyId { get; set; }
        public TransportCompany? TransportCompany { get; set; }
        public decimal? MinWeight { get; set; }
        public decimal? MaxWeight { get; set; }
        public decimal? BaseCost { get; set; }
        public decimal? ExtraCostPerKg { get; set; }
        public decimal? ExtraCostDifficult { get; set; }
        public decimal? CubicRate { get; set; }
        public decimal? MinCharge { get; set; }
    }
}
