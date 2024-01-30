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
        private readonly IMapper _mapper;

        public WalkController(IWalkRepository walkRepo, IMapper mapper)
        {
            _walkRepo = walkRepo;
            _mapper = mapper;
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
    }
}
