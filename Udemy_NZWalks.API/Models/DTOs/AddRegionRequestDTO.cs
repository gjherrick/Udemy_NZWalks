namespace Udemy_NZWalks.API.Models.DTOs
{
    public class AddRegionRequestDTO
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; } //nullable property made by the [?]
    }
}
