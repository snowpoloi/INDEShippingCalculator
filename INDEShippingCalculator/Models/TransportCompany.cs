namespace INDEShipping.Models
{
    public class TransportCompany
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // Αρχικοποιούμε το Name με κενή συμβολοσειρά για να μην είναι null
        public decimal MaxLength { get; set; }
        public decimal MaxWidth { get; set; }
        public decimal MaxHeight { get; set; }
        public decimal MaxWeight { get; set; }
        public decimal MaxCubic { get; set; }
        public string OfferType { get; set; } = string.Empty; // Αρχικοποιούμε το OfferType με κενή συμβολοσειρά για να μην είναι null

        // Εδώ προσθέτουμε τα πεδία για την αντιστοίχιση των περιοχών με XML
        public ICollection<PostalCode> ServicedAreas { get; set; } = new List<PostalCode>();
        public ICollection<PostalCode> DifficultAreas { get; set; } = new List<PostalCode>();
        public ICollection<PostalCode> NoCodAreas { get; set; } = new List<PostalCode>();
    }
}
