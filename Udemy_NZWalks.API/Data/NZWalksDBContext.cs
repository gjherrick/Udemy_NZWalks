using Microsoft.EntityFrameworkCore;
using Udemy_NZWalks.API.Models.Domain;

namespace Udemy_NZWalks.API.Data
{
    public class Udemy_NZWalksDBContext: DbContext
    {
        public Udemy_NZWalksDBContext(DbContextOptions dbContextOptions): base(dbContextOptions)
            //ctor +double tab
        {
                
        }

        //these connect to the database
        public  DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Region { get; set; }

        public DbSet<Walk> Walks { get; set; }
    }
}
