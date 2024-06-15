using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using INDEShipping.Data;
using INDEShipping.Models;
using System.Threading.Tasks;
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

        // GET: Offers
        public async Task<IActionResult> Index()
        {
            var offers = await _context.Offers.Include(o => o.TransportCompany).ToListAsync();
            return View(offers);
        }

        // GET: Offers/Create
        public IActionResult Create()
        {
            ViewBag.TransportCompanyId = new SelectList(_context.TransportCompanies, "Id", "Name");
            return View();
        }

        // POST: Offers/Create
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
            ViewBag.TransportCompanyId = new SelectList(_context.TransportCompanies, "Id", "Name", offer.TransportCompanyId);
            return View(offer);
        }

        // Υπολογισμός Κόστους Αποστολής
        [HttpPost]
        public async Task<IActionResult> CalculateShippingCost(int transportCompanyId, decimal weight, decimal volume, string postalCode)
        {
            var company = await _context.TransportCompanies.FindAsync(transportCompanyId);
            if (company == null)
            {
                return NotFound();
            }

            var offers = await _context.Offers
                .Where(o => o.TransportCompanyId == transportCompanyId)
                .ToListAsync();

            decimal cost = 0;

            foreach (var offer in offers)
            {
                if (weight >= (offer.MinWeight ?? 0) && weight <= (offer.MaxWeight ?? decimal.MaxValue))
                {
                    cost = (offer.BaseCost ?? 0) + (weight * (offer.ExtraCostPerKg ?? 0));
                    if (IsDifficultAccess(postalCode))
                    {
                        cost += (offer.ExtraCostDifficult ?? 0);
                    }
                    break;
                }
            }

            // Αν δεν βρέθηκε προσφορά για βάρος, ελέγξτε τον όγκο
            if (cost == 0)
            {
                foreach (var offer in offers)
                {
                    if (volume >= (offer.MinCharge ?? 0))
                    {
                        cost = (offer.CubicRate ?? 0) * volume;
                        if (IsDifficultAccess(postalCode))
                        {
                            cost += (offer.ExtraCostDifficult ?? 0);
                        }
                        break;
                    }
                }
            }

            return Json(new { cost = cost });
        }

        private bool IsDifficultAccess(string postalCode)
        {
            var postalCodeRecord = _context.PostalCodes.FirstOrDefault(p => p.Code == postalCode);
            return postalCodeRecord?.IsDifficultAccess ?? false;
        }

        public IActionResult Calculate()
        {
            ViewBag.TransportCompanies = _context.TransportCompanies.ToList();
            return View();
        }
    }
}
