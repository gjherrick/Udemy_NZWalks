using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Udemy_NZWalks.API.Data;
using Udemy_NZWalks.API.Models.Domain;
using Udemy_NZWalks.API.Models.DTOs;
using Udemy_NZWalks.API.Repositories;


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
        private readonly iRegionRepository regionRepository;

        public RegionsController(Udemy_NZWalksDBContext dbContext, iRegionRepository regionRepository)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
        }


        //brings up the overall get function. GETS ALL REGIONS
        [HttpGet] 
        public async Task<IActionResult> GetAll() //Getting all the resources
        {
            var regions = await regionRepository.GetAllAsync(); 
            //this abstracts the call so regionRep is a layer between controller and info

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
        public async Task<IActionResult> GetById([FromRoute] Guid id)

        {
            //var region = dbContext.Region.Find(id);
            //get region domain model from database

            var region = await dbContext.Region.FirstOrDefaultAsync(x => x.Id == id);//firstordefault must be Async
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

        //POST TO CREATE NEW REGION
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDTO addRegionRequestDTO)//frombody comes from POST
        {
            //map or convert DTO to domain Model
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDTO.Code,
                Name = addRegionRequestDTO.Name,
                RegionImageUrl = addRegionRequestDTO.RegionImageUrl
            };

            //Use domain model to create Region
             await dbContext.Region.AddAsync(regionDomainModel);
             await dbContext.SaveChangesAsync();

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


        //updating a particular region by ID
        //PUT: https://localhost:portnumber/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO) //using this with update functionality
                           //  Using id property to check if region exists, then use updateDTO info 
                           //In the FromBody you need to put the UpdateDTO first for the class
                           //and then 2nd for the variable name. Class is U and variable is u.
        {
            var regionDomainModel = await dbContext.Region.FirstOrDefaultAsync(r => r.Id == id); //r or x var name doesnt matter tbh
            //is allowed to be null if the ID is wrong.
            
            
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //if region IS found, map DTO back to domain model.
            regionDomainModel.Code = updateRegionRequestDTO.Code;
            regionDomainModel.Name = updateRegionRequestDTO.Name;
            regionDomainModel.RegionImageUrl = updateRegionRequestDTO?.RegionImageUrl;

            await dbContext.SaveChangesAsync();

            //convert domain model to DTO
            var regionDTO = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return Ok(regionDTO); //Always return DTOs to the client, not Domains
        }

        //DELETE REGION
        //DElETE: https://localhost:portnumber/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await dbContext.Region.FirstOrDefaultAsync(r => r.Id == id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }
            //delete the region
            dbContext.Region.Remove(regionDomainModel); //remove is a synchronous method, no async version exists.
            await dbContext.SaveChangesAsync();

            //return deleted Region back
            //map domain model to DTO
            var regionDTO = new RegionDTO //copy pasted from above for ease of use.
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };


            return Ok(regionDTO);

        }

    }
}

