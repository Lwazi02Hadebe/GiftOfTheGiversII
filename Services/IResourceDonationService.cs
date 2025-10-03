using GiftOfTheGiversII.Models;

namespace GiftOfTheGiversII.Services
{
    public interface IResourceDonationService
    {
        Task<ResourceDonation> CreateResourceDonationAsync(int itemId, int quantity, int? projectId, int? userId, string deliveryMethod, string? notes, bool isAnonymous);
        Task<List<ResourceDonation>> GetUserResourceDonationsAsync(int userId);
        Task<List<ResourceDonation>> GetAllResourceDonationsAsync();
        Task<List<DonationCenter>> GetDonationCentersAsync();
        Task<List<InventoryItem>> GetAvailableItemsAsync();
        Task UpdateDonationStatusAsync(int donationId, string status);
        Task<int> GetTotalResourcesDonatedAsync();
        Task<List<ResourceDonation>> GetProjectResourceDonationsAsync(int projectId);
    }
}