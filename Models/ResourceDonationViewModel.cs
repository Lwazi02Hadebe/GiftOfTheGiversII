namespace GiftOfTheGiversII.Models
{
    public class ResourceDonationViewModel
    {
        public int ItemID { get; set; }
        public int Quantity { get; set; }
        public int? ProjectID { get; set; }
        public string DeliveryMethod { get; set; } = string.Empty;
        public string? Notes { get; set; }
        public bool IsAnonymous { get; set; }
        public List<InventoryItem>? AvailableItems { get; set; }
        public List<DonationCenter>? DonationCenters { get; set; }
        public ReliefProject? Project { get; set; }
    }
}
