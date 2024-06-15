using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace INDEShipping.ViewModels
{
    public class XmlFieldMatchViewModel
    {
        public List<string> XmlFields { get; set; } = new List<string>();
        public SelectList DatabaseFields { get; set; } = new SelectList(new List<string>());
        public List<FieldMapping> FieldMappings { get; set; } = new List<FieldMapping>();
    }

    public class FieldMapping
    {
        public string? XmlField { get; set; }
        public string? DatabaseField { get; set; }
    }
}
