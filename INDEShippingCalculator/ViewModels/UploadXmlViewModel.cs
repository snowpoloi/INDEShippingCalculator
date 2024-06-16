using Microsoft.AspNetCore.Mvc.Rendering;

namespace INDEShipping.ViewModels
{
    public class UploadXmlViewModel
    {
        public int TransportCompanyId { get; set; }
        public string XmlType { get; set; }
        public IFormFile File { get; set; }
    }
}
