using GiftOfTheGiversII.Models;
using GiftOfTheGiversII.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GiftOfTheGiversII.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IDonationService _donationService;

        public HomeController(ApplicationDbContext context, IDonationService donationService)
        {
            _context = context;
            _donationService = donationService;
        }

        public async Task<IActionResult> Index()
        {
            var projects = await _context.ReliefProjects
                .Include(p => p.ProjectNeeds)
                .ThenInclude(pn => pn.Item)
                .Where(p => p.Status == "Active")
                .ToListAsync();

            ViewData["TotalDonations"] = await _donationService.GetTotalDonationsAsync();
            return View(projects);
        }

        public async Task<IActionResult> ProjectDetails(int id)
        {
            var project = await _context.ReliefProjects
                .Include(p => p.ProjectNeeds)
                .ThenInclude(pn => pn.Item)
                .FirstOrDefaultAsync(p => p.ProjectID == id);

            if (project == null)
                return NotFound();

            return View(project);
        }
        public async Task<IActionResult> Projects()
        {
            var projects = await _context.ReliefProjects
                .Include(p => p.ProjectNeeds)
                .ThenInclude(pn => pn.Item)
                .ToListAsync();
            return View(projects);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class ErrorViewModel
    {
        public string? RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
