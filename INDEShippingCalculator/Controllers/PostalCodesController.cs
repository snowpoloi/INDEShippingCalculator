using Microsoft.AspNetCore.Mvc;
using INDEShipping.Data;
using INDEShipping.Models;
using INDEShipping.ViewModels; // Βεβαιωθείτε ότι αυτή η δήλωση υπάρχει
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace INDEShipping.Controllers
{
    public class PostalCodesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostalCodesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult UploadXml()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadXml(IFormFile xmlFile)
        {
            if (xmlFile != null && xmlFile.Length > 0)
            {
                using (var stream = xmlFile.OpenReadStream())
                {
                    var xmlDoc = XDocument.Load(stream);

                    var postalCodes = from pc in xmlDoc.Descendants("PostalCode")
                                      select new PostalCode
                                      {
                                          Code = pc.Element("Code")?.Value,
                                          Nomos = pc.Element("Nomos")?.Value,
                                          City = pc.Element("City")?.Value,
                                          Area = pc.Element("Area")?.Value,
                                          IsDifficultAccess = bool.Parse(pc.Element("IsDifficultAccess")?.Value ?? "false"),
                                          NoCOD = bool.Parse(pc.Element("NoCOD")?.Value ?? "false")
                                      };

                    _context.PostalCodes.AddRange(postalCodes);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
            }

            return View();
        }

        public IActionResult MatchFields()
        {
            var xmlFields = new List<string> { "Code", "Nomos", "City", "Area", "IsDifficultAccess", "NoCOD" };
            var databaseFields = new SelectList(new List<string> { "Code", "Nomos", "City", "Area", "IsDifficultAccess", "NoCOD" });

            var model = new XmlFieldMatchViewModel
            {
                XmlFields = xmlFields,
                DatabaseFields = databaseFields,
                FieldMappings = xmlFields.Select(x => new ViewModels.FieldMapping()).ToList()
            };

            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MatchFields(XmlFieldMatchViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
    }
}
