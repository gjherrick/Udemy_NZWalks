using Udemy_NZWalks.API.Models.Domain;

namespace Udemy_NZWalks.API.Repositories
{
    public class InMemoryRegionRepository : iRegionRepository
    {
        public Task<List<Region>> GetAllAsync()
        {
           return List<Region>()
            {
                new Region()
                {
                    Id = Guid.NewGuid(),
                    Code = 'Sameer'switch 
                }
            }
        }
    }
}
