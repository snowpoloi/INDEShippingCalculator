using Microsoft.AspNetCore.Mvc;
using INDEShipping.Data;
using INDEShipping.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace INDEShipping.Controllers
{
    public class OffersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OffersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var offers = await _context.Offers.Include(o => o.TransportCompany).ToListAsync();
            return View(offers);
        }

        public IActionResult Create()
        {
            ViewData["TransportCompanyId"] = new SelectList(_context.TransportCompanies, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TransportCompanyId,MinWeight,MaxWeight,BaseCost,ExtraCostPerKg,ExtraCostDifficult,CubicRate,MinCharge")] Offer offer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(offer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TransportCompanyId"] = new SelectList(_context.TransportCompanies, "Id", "Name", offer.TransportCompanyId);
            return View(offer);
        }

        // Add Edit, Details, Delete actions as needed
    }
}
