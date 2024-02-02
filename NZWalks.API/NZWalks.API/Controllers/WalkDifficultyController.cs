using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkDifficultyController : ControllerBase
    {
        private readonly IWalkDifficultyRepository _wDifficultyRepo;
        private readonly IMapper _mapper;

        public WalkDifficultyController(IWalkDifficultyRepository wDifficultyRepo, IMapper mapper)
        {
            _wDifficultyRepo = wDifficultyRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWDifficulty()
        {
            var difficulties = await _wDifficultyRepo.GetAllAsync();
            var difficultiesDTO = _mapper.Map<List<WalkDifficultyDTO>>(difficulties);

            return Ok(difficultiesDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetWDifficulty(Guid id) 
        {
            var difficulty = await _wDifficultyRepo.GetAsync(id);
            var difficultyDTO = _mapper.Map<WalkDifficultyDTO>(difficulty);

            return Ok(difficultyDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWDifficulty(AddWDifficultyRequest addRequest)
        {
            var newDifficulty = new WalkDifficulty
            {
                Code = addRequest.Code
            };

            var difficulty = await _wDifficultyRepo.AddAsync(newDifficulty);
            var difficultyDTO = _mapper.Map<WalkDifficultyDTO>(difficulty);
            return CreatedAtAction(nameof(GetWDifficulty), new { id = difficultyDTO.Id }, difficultyDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWDifficulty(Guid id, UpdateWDifficultyRequest updateRequest)
        {
            var difficulty = new WalkDifficulty
            {
                Code = updateRequest.Code
            };

            var difficultyFromDb = await _wDifficultyRepo.UpdateAsync(id, difficulty);

            if (difficultyFromDb == null)
            {
                return NotFound();
            }

            var difficultyDTO = _mapper.Map<WalkDifficultyDTO>(difficultyFromDb);
            return Ok(difficultyDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWDifficulty(Guid id)
        {
            var difficultyFromDb = await _wDifficultyRepo.DeleteAsync(id);

            if (difficultyFromDb == null)
            {
                return NotFound();
            }

            var difficultyDTO = _mapper.Map<WalkDifficultyDTO>(difficultyFromDb);
            return Ok(difficultyDTO);
        }
    }
}
