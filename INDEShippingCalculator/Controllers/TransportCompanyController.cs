using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using INDEShipping.Data;
using INDEShipping.Models;
using INDEShipping.ViewModels;

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

        // GET: TransportCompany/Details/5
        public async Task<IActionResult> Details(int? id)
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
            _context.TransportCompanies.Remove(transportCompany ?? new TransportCompany());
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransportCompanyExists(int id)
        {
            return _context.TransportCompanies.Any(e => e.Id == id);
        }

        // GET: TransportCompany/UploadXml
        public IActionResult UploadXml()
        {
            return View(new UploadXmlViewModel());
        }

        // POST: TransportCompany/UploadXml
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadXml(UploadXmlViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Logic to process the uploaded XML file
                // var xmlFields = ProcessXmlFile(model.File);
                var xmlFields = new List<string> { "Field1", "Field2", "Field3" }; // Mock data for example
                var viewModel = new XmlFieldMatchViewModel
                {
                    XmlFields = xmlFields,
                    DatabaseFields = new SelectList(new List<string> { "DatabaseField1", "DatabaseField2" })
                };
                return View("MatchFields", viewModel);
            }
            return View(model);
        }

        // POST: TransportCompany/MatchFields
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MatchFields(XmlFieldMatchViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Logic to save field mappings to the database
                foreach (var mapping in model.FieldMappings ?? new List<FieldMapping>())
                {
                    var xmlField = mapping.XmlField ?? string.Empty;
                    var dbField = mapping.DatabaseField ?? string.Empty;
                    // Add your logic here to handle
