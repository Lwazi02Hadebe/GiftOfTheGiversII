using GiftOfTheGiversII.Models;
using GiftOfTheGiversII.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GiftOfTheGiversII.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IDonationService _donationService;

        public AdminController(ApplicationDbContext context, IDonationService donationService)
        {
            _context = context;
            _donationService = donationService;
        }

        public async Task<IActionResult> Dashboard()
        {
            ViewBag.TotalDonations = await _donationService.GetTotalDonationsAsync();
            ViewBag.TotalUsers = await _context.Users.CountAsync();
            ViewBag.ActiveProjects = await _context.ReliefProjects.CountAsync(p => p.Status == "Active");

            return View();
        }

        public async Task<IActionResult> Users()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }

        public async Task<IActionResult> Donations()
        {
            var donations = await _donationService.GetAllDonationsAsync();
            return View(donations);
        }

        public async Task<IActionResult> Projects()
        {
            var projects = await _context.ReliefProjects
                .Include(p => p.ProjectNeeds)
                .ThenInclude(pn => pn.Item)
                .ToListAsync();
            return View(projects);
        }

        [HttpGet]
        public IActionResult CreateProject()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject(ReliefProject project)
        {
            if (ModelState.IsValid)
            {
                _context.ReliefProjects.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction("Projects");
            }
            return View(project);
        }
    }
}

