using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.CustomActionFilters;
using NZWalks.Data;
using NZWalks.Mappings;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;
using NZWalks.Repositories;

namespace NZWalks.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegionController : Controller
    {
        //private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionController(IRegionRepository _regionRepository, IMapper _mapper )
        {
            
            this.regionRepository = _regionRepository;
            this.mapper = _mapper;
        }

        #region HttpGet GetAllRegions
        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
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
            //var regions = await dbContext.Regions.ToListAsync();
            var regions = await regionRepository.GetAllRegionsAsync();

            //map domain models to DTOs
            //var regionDtos = new List<RegionDto>();
            //foreach (var region in regions)
            //{
            //    regionDtos.Add(new RegionDto
            //    {
            //        Id = region.Id,
            //        Code = region.Code,
            //        Name = region.Name,
            //        RegionImageUrl = region.RegionImageUrl
            //    });
            //}
            var regionDto = mapper.Map<List<RegionDto>>(regions);

            return Ok(regionDto);
        }
        #endregion

        #region HttpGet GetRegionById
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetRegionById(Guid id)
        {
            //Find can be used when we are searching based on primary key, it will return null if no record is found
            //var regionById = dbContext.Regions.Find(id);

            //FirstOrDefault will return the first record that matches the condition, if no record is found it will return null
            //It is a LINQ

            //get data from database using domain model
            //var regionById = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            var regionById = await regionRepository.GetRegionByIdAsync(id);
            if (regionById == null)
            {
                return NotFound();
            }

            //map domain model to DTO
            //var regionByIdDto = new RegionDto
            //{
            //    Id = regionById.Id,
            //    Code = regionById.Code,
            //    Name = regionById.Name,
            //    RegionImageUrl = regionById.RegionImageUrl
            //};
            var regionByIdDto = mapper.Map<RegionDto>(regionById);

            return Ok(regionByIdDto);
        }
        #endregion

        #region HttpPost AddNewRegion
        [HttpPost]
        [ValidateModelAttribute]
        public async Task<IActionResult> AddRegion([FromBody] AddRegionRequestDto addNewRegionDto)
        {
            //map dto to domain model
            //var region = new Region
            //{
            //    Code = addNewRegionDto.Code,
            //    Name = addNewRegionDto.Name,
            //    RegionImageUrl = addNewRegionDto.RegionImageUrl
            //};
           
            var region = mapper.Map<Region>(addNewRegionDto);

            //save the data to database
            //await dbContext.Regions.AddAsync(region);
            //await dbContext.SaveChangesAsync();
            await regionRepository.AddRegionAsync(region);

            //map domain model to dto
            //var regionDto = new RegionDto
            //{
            //    Id = region.Id,
            //    Code = region.Code,
            //    Name = region.Name,
            //    RegionImageUrl = region.RegionImageUrl
            //};
            var regionDto = mapper.Map<RegionDto>(region);

            return CreatedAtAction(nameof(GetRegionById), new { id = regionDto.Id }, regionDto);
        }

        #endregion

        #region HttpPut UpdateRegion
        [HttpPut]
        [Route("{id}")]
        [ValidateModelAttribute]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionDto)
        {
            //get data from database and check if it exists
            //var regionById = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            //if (regionById == null)
            //{
            //    return NotFound();
            //}

            ////map dto to domain model
            //regionById.Code = updateRegionDto.Code;
            //regionById.Name = updateRegionDto.Name;
            //regionById.RegionImageUrl = updateRegionDto.RegionImageUrl;

            ////save the data to database
            //await dbContext.SaveChangesAsync();
            //map dto to domain model
            //var regionDomainModel = new Region
            //{
            //    Code = updateRegionDto.Code,
            //    Name = updateRegionDto.Name,
            //    RegionImageUrl = updateRegionDto.RegionImageUrl

            //};
            var regionDomainModel = mapper.Map<Region>(updateRegionDto);
            regionDomainModel = await regionRepository.UpdateRegionAsync(id, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //map domain model to dto
            //var regionDto = new RegionDto
            //{
            //    Id = regionDomainModel.Id,
            //    Code = regionDomainModel.Code,
            //    Name = regionDomainModel.Name,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl
            //};
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return Ok(regionDto);
        }

        #endregion

        #region HttpDelete DeleteRegion
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            //check if data exists or not
            //var regionById = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            var region = await regionRepository.DeleteRegionAsync(id);
            if (region == null)
            {
                return NotFound();
            }

            //dbContext.Regions.Remove(regionById);
            //await dbContext.SaveChangesAsync();

            return Ok();
        }

        #endregion
    }
}
