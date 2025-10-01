using GiftOfTheGiversII.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GiftOfTheGiversII.Controllers
{
    [Authorize]
    public class DonationController : Controller
    {
        private readonly IDonationService _donationService;

        public DonationController(IDonationService donationService)
        {
            _donationService = donationService;
        }

        [HttpGet]
        public IActionResult MakeDonation()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> MakeDonation(decimal amount, string paymentMethod, bool isAnonymous = false)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var donation = await _donationService.CreateDonationAsync(amount, paymentMethod, userId, isAnonymous);

            ViewBag.Success = $"Thank you for your donation of R{amount}!";
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> MyDonations()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var donations = await _donationService.GetUserDonationsAsync(userId);
            return View(donations);
        }
    }
}