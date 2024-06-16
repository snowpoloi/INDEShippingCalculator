using Microsoft.AspNetCore.Http;
using System.Xml;
using System.Collections.Generic;
using INDEShipping.ViewModels;
using INDEShipping.Data;
using INDEShipping.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;

public class TransportCompanyController : Controller
{
    private readonly ApplicationDbContext _context;

    public TransportCompanyController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult UploadXml()
    {
        ViewBag.TransportCompanies = new SelectList(_context.TransportCompanies, "Id", "Name");
        return View();
    }

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
