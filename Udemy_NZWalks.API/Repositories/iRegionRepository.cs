using Udemy_NZWalks.API.Models.Domain;
namespace Udemy_NZWalks.API.Repositories
{
    public interface iRegionRepository //the RegionsController has the CRUD + Get all + Control methods
    {
        //need 5 definitions of methods we will later run implemented in a concrete class.
        Task<List<Region>> GetAllAsync(); //return list of regions back from the db

    }

}
