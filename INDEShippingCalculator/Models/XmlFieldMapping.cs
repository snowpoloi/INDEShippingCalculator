using System.ComponentModel.DataAnnotations;

namespace INDEShipping.Models
{
    public class XmlFieldMapping
    {
        public int Id { get; set; }
        [Required]
        public string XmlField { get; set; } = string.Empty;
        [Required]
        public string DatabaseField { get; set; } = string.Empty;
    }
}
