namespace Udemy_NZWalks.API.Models.DTOs
{
    public class RegionDTO //can be a subset, where it can have one or multiple properties of the parent

    {
        //Pasted from Region.cs. DTOs are airlock doors for information.
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; } //nullable property made by the [?]
    }
}
