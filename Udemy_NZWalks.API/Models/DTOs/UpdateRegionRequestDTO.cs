namespace Udemy_NZWalks.API.Models.DTOs
{
    public class UpdateRegionRequestDTO //these are the proprties the DTO can affect
    {
        public string Code { get; set; } //client can update the code, name and image.
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; } //nullable property made by the [?]
    }
}
