
using GiftOfTheGiversII.Models;
using Microsoft.EntityFrameworkCore;

namespace GiftOfTheGiversII.Services
{
    public interface IDonationService
    {
        Task<Donation> CreateDonationAsync(decimal amount, string paymentMethod, int? userId = null, bool isAnonymous = false);
        Task<List<Donation>> GetUserDonationsAsync(int userId);
        Task<List<Donation>> GetAllDonationsAsync();
        Task<decimal> GetTotalDonationsAsync();
    }

    public class DonationService : IDonationService
    {
        private readonly ApplicationDbContext _context;

        public DonationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Donation> CreateDonationAsync(decimal amount, string paymentMethod, int? userId = null, bool isAnonymous = false)
        {
            var donation = new Donation
            {
                UserID = userId,
                Amount = amount,
                PaymentMethod = paymentMethod,
                Status = "Completed",
                DonationDate = DateTime.UtcNow,
                IsAnonymous = isAnonymous,
                PaymentTransactionID = Guid.NewGuid().ToString()
            };

            _context.Donations.Add(donation);
            await _context.SaveChangesAsync();
            return donation;
        }

        public async Task<List<Donation>> GetUserDonationsAsync(int userId)
        {
            return await _context.Donations
                .Where(d => d.UserID == userId)
                .OrderByDescending(d => d.DonationDate)
                .ToListAsync();
        }

        public async Task<List<Donation>> GetAllDonationsAsync()
        {
            return await _context.Donations
                .Include(d => d.User)
                .OrderByDescending(d => d.DonationDate)
                .ToListAsync();
        }

        public async Task<decimal> GetTotalDonationsAsync()
        {
            return await _context.Donations
                .Where(d => d.Status == "Completed")
                .SumAsync(d => d.Amount);
        }
    }
}
