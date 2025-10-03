using GiftOfTheGiversII.Models;
using GiftOfTheGiversII.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GiftOfTheGiversII.Controllers
{
    public class ResourceDonationController : Controller
    {
        private readonly IResourceDonationService _resourceDonationService;
        private readonly ApplicationDbContext _context;

        public ResourceDonationController(IResourceDonationService resourceDonationService, ApplicationDbContext context)
        {
            _resourceDonationService = resourceDonationService;
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> DonationCenters()
        {
            var centers = await _resourceDonationService.GetDonationCentersAsync();
            return View(centers);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> MakeResourceDonation(int? projectId = null)
        {
            ViewBag.AvailableItems = await _resourceDonationService.GetAvailableItemsAsync();
            ViewBag.DonationCenters = await _resourceDonationService.GetDonationCentersAsync();

            if (projectId.HasValue)
            {
                var project = await _context.ReliefProjects
                    .Include(p => p.ProjectNeeds)
                    .ThenInclude(pn => pn.Item)
                    .FirstOrDefaultAsync(p => p.ProjectID == projectId);
                ViewBag.Project = project;
            }

            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> MakeResourceDonation(int itemId, int quantity, int? projectId, string deliveryMethod, string? notes, bool isAnonymous = false)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var donation = await _resourceDonationService.CreateResourceDonationAsync(
                itemId, quantity, projectId, userId, deliveryMethod, notes, isAnonymous);

            ViewBag.Success = $"Thank you for your resource donation! Your donation ID is RD{donation.ResourceDonationID:0000}.";
            ViewBag.AvailableItems = await _resourceDonationService.GetAvailableItemsAsync();
            ViewBag.DonationCenters = await _resourceDonationService.GetDonationCentersAsync();

            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> MyResourceDonations()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var donations = await _resourceDonationService.GetUserResourceDonationsAsync(userId);
            return View(donations);
        }
    }
}
