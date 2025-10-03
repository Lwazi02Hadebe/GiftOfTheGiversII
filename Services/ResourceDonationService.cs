
using GiftOfTheGiversII.Models;
using Microsoft.EntityFrameworkCore;

namespace GiftOfTheGiversII.Services
{
    public class ResourceDonationService : IResourceDonationService
    {
        private readonly ApplicationDbContext _context;

        public ResourceDonationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResourceDonation> CreateResourceDonationAsync(int itemId, int quantity, int? projectId, int? userId, string deliveryMethod, string? notes, bool isAnonymous)
        {
            var donation = new ResourceDonation
            {
                ItemID = itemId,
                Quantity = quantity,
                ProjectID = projectId,
                UserID = userId,
                DeliveryMethod = deliveryMethod,
                Notes = notes,
                IsAnonymous = isAnonymous,
                Status = "Pending",
                DonationDate = DateTime.UtcNow
            };

            _context.ResourceDonations.Add(donation);
            await _context.SaveChangesAsync();

            return donation;
        }

        public async Task<List<ResourceDonation>> GetUserResourceDonationsAsync(int userId)
        {
            return await _context.ResourceDonations
                .Include(rd => rd.Item)
                .Include(rd => rd.Project)
                .Where(rd => rd.UserID == userId)
                .OrderByDescending(rd => rd.DonationDate)
                .ToListAsync();
        }

        public async Task<List<ResourceDonation>> GetAllResourceDonationsAsync()
        {
            return await _context.ResourceDonations
                .Include(rd => rd.Item)
                .Include(rd => rd.Project)
                .Include(rd => rd.User)
                .OrderByDescending(rd => rd.DonationDate)
                .ToListAsync();
        }

        public async Task<List<DonationCenter>> GetDonationCentersAsync()
        {
            return await _context.DonationCenters
                .Include(dc => dc.AcceptedItems)
                .ThenInclude(ai => ai.Item)
                .Where(dc => dc.IsActive)
                .ToListAsync();
        }

        public async Task<List<InventoryItem>> GetAvailableItemsAsync()
        {
            return await _context.InventoryItems.ToListAsync();
        }

        public async Task UpdateDonationStatusAsync(int donationId, string status)
        {
            var donation = await _context.ResourceDonations.FindAsync(donationId);
            if (donation != null)
            {
                donation.Status = status;
                if (status == "Received")
                {
                    donation.ReceivedDate = DateTime.UtcNow;
                }
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> GetTotalResourcesDonatedAsync()
        {
            return await _context.ResourceDonations
                .Where(rd => rd.Status == "Received" || rd.Status == "Distributed")
                .SumAsync(rd => rd.Quantity);
        }

        public async Task<List<ResourceDonation>> GetProjectResourceDonationsAsync(int projectId)
        {
            return await _context.ResourceDonations
                .Include(rd => rd.Item)
                .Include(rd => rd.User)
                .Where(rd => rd.ProjectID == projectId)
                .OrderByDescending(rd => rd.DonationDate)
                .ToListAsync();
        }
    }
}