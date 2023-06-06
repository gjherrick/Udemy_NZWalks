using System.Reflection.Metadata.Ecma335;

namespace Udemy_NZWalks.API.Models.Domain
{
    public class Region
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; } //nullable property made by the [?]
    }
}
