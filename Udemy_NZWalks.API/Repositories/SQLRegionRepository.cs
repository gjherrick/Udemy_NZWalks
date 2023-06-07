using Microsoft.EntityFrameworkCore;
using Udemy_NZWalks.API.Data;
using Udemy_NZWalks.API.Models.Domain;

namespace Udemy_NZWalks.API.Repositories
{
    public class SQLRegionRepository : iRegionRepository
    {
        private readonly Udemy_NZWalksDBContext dbContext;
        public SQLRegionRepository(Udemy_NZWalksDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Region>> GetAllAsync()
        {
         return await dbContext.Region.ToListAsync();
        }
    }
}
