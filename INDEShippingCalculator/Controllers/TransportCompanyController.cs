using Microsoft.AspNetCore.Mvc;
using INDEShipping.Data;
using INDEShipping.Models;
using INDEShipping.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using System.Collections.Generic;
using System.IO;

namespace INDEShipping.Controllers
{
    public class TransportCompanyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TransportCompanyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TransportCompany
        public async Task<IActionResult> Index()
        {
            return View(await _context.TransportCompanies.ToListAsync());
        }

        // GET: TransportCompany/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TransportCompany/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,MaxLength,MaxWidth,MaxHeight,MaxWeight,MaxCubic,OfferType")] TransportCompany transportCompany)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transportCompany);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transportCompany);
        }

        // GET: TransportCompany/UploadXml
        public IActionResult UploadXml()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadXml(IFormFile xmlFile)
        {
            if (xmlFile == null || xmlFile.Length == 0)
            {
                return BadRequest("Please upload a valid XML file.");
            }

            List<string> xmlFields;
            using (var stream = xmlFile.OpenReadStream())
            {
                var xmlDoc = XDocument.Load(stream);
                xmlFields = xmlDoc.Descendants().Select(e => e.Name.LocalName).Distinct().ToList();
            }

            var databaseFields = new SelectList(new List<string> { "Code", "Nomos", "City", "Area", "IsDifficultAccess", "NoCOD" });

            var viewModel = new XmlFieldMatchViewModel
            {
                XmlFields = xmlFields,
                DatabaseFields = databaseFields
            };

            return View("MatchFields", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> MatchFields(XmlFieldMatchViewModel model)
        {
            foreach (var mapping in model.FieldMappings)
            {
                if (mapping.XmlField != null && mapping.DatabaseField != null)
                {
                    var xmlFieldMapping = new XmlFieldMapping
                    {
                        XmlField = mapping.XmlField,
                        DatabaseField = mapping.DatabaseField
                    };
                    _context.XmlFieldMappings.Add(xmlFieldMapping);
                }
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
