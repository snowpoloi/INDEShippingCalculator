namespace INDEShipping.Models
{
    public class PostalCode
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Nomos { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Area { get; set; } = string.Empty;
    }
}
