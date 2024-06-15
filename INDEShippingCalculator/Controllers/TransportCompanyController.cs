using INDEShipping.Data;
using INDEShipping.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
