using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.CustomActionFilters;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;
using NZWalks.Repositories;

namespace NZWalks.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalkController : Controller
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;
        public WalkController(IMapper _mapper, IWalkRepository _walkRepository)
        {
            mapper = _mapper;
            walkRepository = _walkRepository;
        }
        #region HttpPost AddWalk
        [HttpPost]
        [ValidateModelAttribute]
        public async Task<IActionResult> AddWalk([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            //map DTO to domain model
            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

            // Call repository to add walk to database
            var addedWalkDomainModel = await walkRepository.AddWalkAsync(walkDomainModel);

            // Map the added walk domain model back to DTO
            var addedWalkDto = mapper.Map<WalkDto>(addedWalkDomainModel);

            return Ok(addedWalkDto);

        }


        #endregion

        #region HttpGet GetAllWalks
        [HttpGet] 
        public async Task<IActionResult> GetAllWalks()
        {
            // Call repository to get all walks from database
            var walkDomainModel = await walkRepository.GetAllWalksAsync();

            //map domain model to DTO
            var walkDto = mapper.Map<List<WalkDto>>(walkDomainModel);
            return Ok(walkDto);
        }
        #endregion

        #region HttpGet GetWalkById
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetWalkById(Guid id)
        {
            //call repository to get walk by id from database
            var existingWalkDomainModel = await walkRepository.GetWalkByIdAsync(id); 
            if(existingWalkDomainModel == null)
            {
                return NotFound();
            }

            //map domain model to DTO
            var walkDto = mapper.Map<WalkDto>(existingWalkDomainModel); 
            return Ok(walkDto);
        }
        #endregion

        #region HttpPut UpdateWalk
        [HttpPut]
        [Route("{id:guid}")]
        [ValidateModelAttribute]
        public async Task<IActionResult> UpdateWalk([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
        {
            //map dto to domain model
            var updateDomainModel = mapper.Map<Walk>(updateWalkRequestDto);

            //call repository for update 
            updateDomainModel = await walkRepository.UpdateWalkAsync(id, updateDomainModel);
            if (updateDomainModel == null)
            {
                return NotFound();
            }

            //map domain model to dto
            return Ok(mapper.Map<WalkDto>(updateDomainModel));
        }


        #endregion

        #region HttpDelete DeleteWalk
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalk(Guid id)
        {
            //call repository for delete action
            var existingWalkId = await walkRepository.DeleteWalkAsync(id);
            if (existingWalkId == null)
            {
                return NotFound();
            }

            return Ok();
        }

        #endregion
    }
}
