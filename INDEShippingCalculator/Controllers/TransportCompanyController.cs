using Microsoft.AspNetCore.Mvc;
using INDEShipping.Data;
using INDEShipping.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace INDEShipping.Controllers
{
    public class TransportCompanyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TransportCompanyController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.TransportCompanies.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

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

        // Add Edit, Details, Delete actions as needed
    }
}
