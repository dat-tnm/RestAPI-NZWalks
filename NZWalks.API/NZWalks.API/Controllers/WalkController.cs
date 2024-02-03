using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Reflection.Metadata.Ecma335;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkController : ControllerBase
    {
        private readonly IWalkRepository _walkRepo;
        private readonly IRegionRepository _regionRepo;
        private readonly IWalkDifficultyRepository _wDifficultyRepo;
        private readonly IMapper _mapper;

        public WalkController(IWalkRepository walkRepo, IMapper mapper, IRegionRepository regionRepo, IWalkDifficultyRepository wDifficultyRepo)
        {
            _walkRepo = walkRepo;
            _mapper = mapper;
            _regionRepo = regionRepo;
            _wDifficultyRepo = wDifficultyRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalks()
        {
            var walks = await _walkRepo.GetAllAsync();

            var walksDTO = _mapper.Map<List<WalkDTO>>(walks);

            return Ok(walksDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetWalk(Guid id)
        {
            var walk = await _walkRepo.GetAsync(id);

            if (walk == null)
            {
                return NotFound();
            }

            var walkDTO = _mapper.Map<WalkDTO>(walk);

            return Ok(walkDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalk(AddWalkRequest addWalkRequest)
        {
            await ValidateAddWalkRequest(addWalkRequest);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newWalk = new Walk
            {
                Length = addWalkRequest.Length,
                Name = addWalkRequest.Name,
                RegionId = addWalkRequest.RegionId,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId
            };

            var walk = await _walkRepo.AddAsync(newWalk);

            var walkDTO = _mapper.Map<WalkDTO>(walk);

            return CreatedAtAction(nameof(GetWalk), new { id = walkDTO.Id }, walkDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalk(Guid id, UpdateWalkRequest updateWalkRequest)
        {
            await ValidateUpdateWalkRequest(updateWalkRequest);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //convert to domain obj
            var walk = new Walk
            {
                Length = updateWalkRequest.Length,
                Name = updateWalkRequest.Name,
                RegionId = updateWalkRequest.RegionId,
                WalkDifficultyId = updateWalkRequest.WalkDifficultyId,
            };

            //update
            var walkFromDb = await _walkRepo.UpdateAsync(id, walk);
            
            //handle null
            if (walkFromDb == null)
            {
                return NotFound();
            }

            //convert to DTO obj
            var walkDTO = _mapper.Map<WalkDTO>(walkFromDb);

            //return result
            return Ok(walkDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalk(Guid id)
        {
            //delete
            var walkFromDb = await _walkRepo.DeleteAsync(id);

            //handle null
            if (walkFromDb == null)
            {
                return NotFound();
            }

            //convert to DTO obj
            var walkDTO = _mapper.Map<WalkDTO>(walkFromDb);

            //return result
            return Ok(walkDTO);
        }


        #region private_method
        private async Task ValidateAddWalkRequest(AddWalkRequest addWalkRequest)
        {
            if (addWalkRequest == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest),
                    "AddWalkRequest data is required.");
                return;
            }

            var regionFromDb = await _regionRepo.GetAsync(addWalkRequest.RegionId);
            if (regionFromDb == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest.RegionId),
                    $"{nameof(addWalkRequest.RegionId)} is not found.");
            }

            var wDifficultyFromDb = await _wDifficultyRepo.GetAsync(addWalkRequest.WalkDifficultyId);
            if (wDifficultyFromDb == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest.WalkDifficultyId),
                    $"{nameof(addWalkRequest.WalkDifficultyId)} is not found.");
            }
        }

        private async Task ValidateUpdateWalkRequest(UpdateWalkRequest updateWalkRequest)
        {
            if (updateWalkRequest == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequest),
                    "UpdateWalkRequest data is required.");
                return;
            }

            var regionFromDb = await _regionRepo.GetAsync(updateWalkRequest.RegionId);
            if (regionFromDb == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequest.RegionId),
                    $"{nameof(updateWalkRequest.RegionId)} is not found.");
            }

            var wDifficultyFromDb = await _wDifficultyRepo.GetAsync(updateWalkRequest.WalkDifficultyId);
            if (wDifficultyFromDb == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequest.WalkDifficultyId),
                    $"{nameof(updateWalkRequest.WalkDifficultyId)} is not found.");
            }
        }
        #endregion
    }
}
