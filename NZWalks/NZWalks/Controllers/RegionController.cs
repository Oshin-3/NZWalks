using Microsoft.AspNetCore.Mvc;
using NZWalks.Data;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;

namespace NZWalks.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegionController : Controller
    {
        private readonly NZWalksDbContext dbContext;

        public RegionController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        #region HttpGet GetAllRegions
        [HttpGet]
        public IActionResult GetAllRegions()
        {
            #region Hardcoded
            //var regions = new List<Region>
            //{
            //    new Region
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "Auckland",
            //        Code = "AKL",
            //        RegionImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/3e/Auckland_skyline_from_Mt_Eden.jpg/2560px-Auckland_skyline_from_Mt_Eden.jpg"
            //    },
            //    new Region
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "Wellington",
            //        Code = "WLG",
            //        RegionImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5e/Wellington_City.jpg/2560px-Wellington_City.jpg"
            //    },
            //};
            #endregion
            //get the data from the database
            var regions = dbContext.Regions.ToList();

            //map domain models to DTOs
            var regionDtos = new List<RegionDto>();
            foreach (var region in regions)
            {
                regionDtos.Add(new RegionDto
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl
                });
            }
            return Ok(regionDtos);
        }
        #endregion

        #region HttpGet GetRegionById
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetRegionById(Guid id)
        {
            //Find can be used when we are searching based on primary key, it will return null if no record is found
            //var regionById = dbContext.Regions.Find(id);

            //FirstOrDefault will return the first record that matches the condition, if no record is found it will return null
            //It is a LINQ

            //get data from database using domain model
            var regionById = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if (regionById == null)
            {
                return NotFound();
            }

            //map domain model to DTO
            var regionByIdDto = new RegionDto
            {
                Id = regionById.Id,
                Code = regionById.Code,
                Name = regionById.Name,
                RegionImageUrl = regionById.RegionImageUrl
            };

            return Ok(regionByIdDto);
        }
        #endregion

        #region HttpPost AddNewRegion
        [HttpPost]
        public IActionResult AddRegion([FromBody] AddNewRegionDto addNewRegionDto)
        {
            //map dto to domain model
            var region = new Region
            {
                Code = addNewRegionDto.Code,
                Name = addNewRegionDto.Name,
                RegionImageUrl = addNewRegionDto.RegionImageUrl
            };

            //save the data to database
            dbContext.Regions.Add(region);
            dbContext.SaveChanges();

            //map domain model to dto
            var regionDto = new RegionDto
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetRegionById), new { id = regionDto.Id }, regionDto);
        }

        #endregion

        #region HttpPut UpdateRegion
        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionDto updateRegionDto)
        {
            //get data from database and check if it exists
            var regionById = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if (regionById == null)
            {
                return NotFound();
            }

            //map dto to domain model
            regionById.Code = updateRegionDto.Code;
            regionById.Name = updateRegionDto.Name;
            regionById.RegionImageUrl = updateRegionDto.RegionImageUrl;

            //save the data to database
            dbContext.SaveChanges();

            //map domain model to dto
            var regionDto = new RegionDto
            {
                Id = regionById.Id,
                Code = regionById.Code,
                Name = regionById.Name,
                RegionImageUrl = regionById.RegionImageUrl
            };

            return Ok(regionDto);
        }

        #endregion

        #region HttpDelete DeleteRegion
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteRegion([FromRoute] Guid id)
        {
            //check if data exists or not
            var regionById = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if (regionById == null)
            {
                return NotFound();
            }

            dbContext.Regions.Remove(regionById);
            dbContext.SaveChanges();

            return Ok();
        }

        #endregion
    }
}
