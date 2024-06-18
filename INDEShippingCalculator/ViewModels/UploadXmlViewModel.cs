using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace INDEShipping.ViewModels
{
    public class UploadXmlViewModel
    {
        public int TransportCompanyId { get; set; }
        public string? XmlType { get; set; }
        public IFormFile? File { get; set; }
        public List<FieldMapping> FieldMappings { get; set; } = new List<FieldMapping>();
    }

    public class FieldMapping
    {
        public string? XmlField { get; set; }
        public string? DatabaseField { get; set; }
    }
}
