using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Udemy_NZWalks.API.Data;
using Udemy_NZWalks.API.Models.Domain;
using Udemy_NZWalks.API.Models.DTOs;

//domain to DTO back to domain back to DTO oh my!
namespace Udemy_NZWalks.API.Controllers
{
    //EXAMPLE https://localhost:1234/api/regions
    //Would make a route to the regions controller
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly Udemy_NZWalksDBContext dbContext;
        public RegionsController(Udemy_NZWalksDBContext dbContext)
        {
            this.dbContext = dbContext;
        }



        [HttpGet] //brings up the get function
        public IActionResult GetAll() //Getting all the resources
        {
            var regions = dbContext.Region.ToList(); //Region is singular!!

            //Map domain models to DTOs DTO objs are lists

            var regionDTO = new List<RegionDTO>();
            foreach (var region in regions)
            {
                regionDTO.Add(new RegionDTO()
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl

                });
            }


            //return DTOs back to the client.
            return Ok(regions); //returns the list of regions back to the caller
        }

        //GET SINGLE REGION BY ID
        //GET: https://localhost:portnumber/api/region/{id}
        [HttpGet]

        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)

        {
            //var region = dbContext.Region.Find(id);
            //get region domain model from database

            var region = dbContext.Region.FirstOrDefault(x => x.Id == id);
            //does the same as above, but only works if you're passing through the same route.

            if (region == null)
            {
                return NotFound();
            }

            var regionDTO = new RegionDTO()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };
            //Map/Convert region domain model to Region DTO
            return Ok(regionDTO); //returns the DTO instead of the original info.



        }
        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDTO addRegionRequestDTO)//frombody comes from POST
        {
            //map or convert DTO to domain Model
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDTO.Code,
                Name = addRegionRequestDTO.Name,
                RegionImageUrl = addRegionRequestDTO.RegionImageUrl
            };

            //Use domain model to create Region
          dbContext.Region.Add(regionDomainModel);
            dbContext.SaveChanges();

            //map domain model back to dto
            var RegionDTO = new RegionDTO() //have converted the domain BACK to a DTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl=regionDomainModel.RegionImageUrl
            };


            return CreatedAtAction(nameof(GetById), new {id = RegionDTO.Id}, RegionDTO);
        }
    }
}
/* this was fully written out under the GetAll
 * and then made obsolete but I wanted to keep it to refer back to
 * 
 * 
           * var regions = new List<Region>
            {
                new Region
                {
                    Id = Guid.NewGuid(),
                    Name = "Auckland Region",
                    Code = "AKL",
                    RegionImageUrl = "https://wiki.hypixel.net/images/2/27/Minecraft_items_red_bed.png"
                    //I'm substituting minecraft pictures because the ones listed aren't available lmao
                },
                 new Region
                {
                    Id = Guid.NewGuid(),
                    Name = "Wellington Region",
                    Code = "WLG",
                    RegionImageUrl = "https://wiki.hypixel.net/images/4/4f/Minecraft_items_red_mushroom_block.png"
                    //copy paste the region and change the name/code
                }
            }; */
