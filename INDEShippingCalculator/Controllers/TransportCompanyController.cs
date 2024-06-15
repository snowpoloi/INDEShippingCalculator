using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using INDEShipping.Data;
using INDEShipping.Models;
using System.Threading.Tasks;
using System.Linq;
using System.Xml;
using Microsoft.AspNetCore.Http;

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

        // POST: TransportCompany/UploadXml
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadXml(IFormFile xmlFile)
        {
            if (xmlFile != null && xmlFile.Length > 0)
            {
                using (var stream = xmlFile.OpenReadStream())
                {
                    var xmlDoc = new XmlDocument();
                    xmlDoc.Load(stream);

                    // Επεξεργασία του XML αρχείου και αποθήκευση στη βάση δεδομένων
                    // ...

                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
