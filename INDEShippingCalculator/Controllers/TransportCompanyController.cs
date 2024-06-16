using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using INDEShipping.Data;
using INDEShipping.Models;
using INDEShipping.ViewModels;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        // GET: TransportCompany/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transportCompany = await _context.TransportCompanies.FindAsync(id);
            if (transportCompany == null)
            {
                return NotFound();
            }
            return View(transportCompany);
        }

        // POST: TransportCompany/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,MaxLength,MaxWidth,MaxHeight,MaxWeight,MaxCubic,OfferType")] TransportCompany transportCompany)
        {
            if (id != transportCompany.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transportCompany);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransportCompanyExists(transportCompany.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(transportCompany);
        }

        // GET: TransportCompany/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transportCompany = await _context.TransportCompanies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transportCompany == null)
            {
                return NotFound();
            }

            return View(transportCompany);
        }

        // POST: TransportCompany/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transportCompany = await _context.TransportCompanies.FindAsync(id);
            _context.TransportCompanies.Remove(transportCompany);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransportCompanyExists(int id)
        {
            return _context.TransportCompanies.Any(e => e.Id == id);
        }

        // Προσθήκη υποστήριξης για ανέβασμα XML
        // GET: TransportCompany/UploadXml
        public IActionResult UploadXml()
        {
            ViewBag.TransportCompanies = new SelectList(_context.TransportCompanies, "Id", "Name");
            return View();
        }

        // POST: TransportCompany/UploadXml
        [HttpPost]
        public async Task<IActionResult> UploadXml(UploadXmlViewModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                ViewBag.Message = "Please select a valid XML file.";
                ViewBag.TransportCompanies = new SelectList(_context.TransportCompanies, "Id", "Name");
                return View(model);
            }

            var transportCompany = await _context.TransportCompanies.FindAsync(model.TransportCompanyId);
            if (transportCompany == null)
            {
                return NotFound();
            }

            var postalCodes = new List<PostalCode>();

            using (var stream = new StreamReader(model.File.OpenReadStream()))
            {
                var xmlDocument = new XmlDocument();
                xmlDocument.Load(stream);

                foreach (XmlNode node in xmlDocument.SelectNodes("//PostalCode"))
                {
                    var postalCode = new PostalCode
                    {
                        Code = node.SelectSingleNode("Code")?.InnerText,
                        Nomos = node.SelectSingleNode("Nomos")?.InnerText,
                        City = node.SelectSingleNode("City")?.InnerText,
                        Area = node.SelectSingleNode("Area")?.InnerText,
                        IsDifficultAccess = model.XmlType == "DifficultAreas",
                        NoCOD = model.XmlType == "NoCodAreas"
                    };

                    postalCodes.Add(postalCode);
                }
            }

            _context.PostalCodes.AddRange(postalCodes);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
