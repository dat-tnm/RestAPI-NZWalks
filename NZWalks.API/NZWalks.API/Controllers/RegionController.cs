using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionController : ControllerBase
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public RegionController(IRegionRepository regionRepository, IMapper mapper)
        {
            _regionRepository = regionRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            var regions = await _regionRepository.GetAllAsync();

            //var regionsDTO = regions.Select(r => new RegionDTO
            //{
            //    Id = r.Id,
            //    Name = r.Name,
            //    Code = r.Code,
            //    Lat = r.Lat,
            //    Population = r.Population,
            //    Area = r.Area,
            //    Long = r.Long
            //}).ToList();

            var regionsDTO = _mapper.Map<List<RegionDTO>>(regions);

            return Ok(regionsDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetRegion(Guid id) 
        {
            var region = await _regionRepository.GetAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            var regionDTO = _mapper.Map<RegionDTO>(region);
            return Ok(regionDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegion(AddRegionRequest addRegionRequest)
        {
            var region = new Region
            {
                Code = addRegionRequest.Code,
                Name = addRegionRequest.Name,
                Lat = addRegionRequest.Lat,
                Population = addRegionRequest.Population,
                Area = addRegionRequest.Area,
                Long = addRegionRequest.Long,
            };

            await _regionRepository.AddAsync(region);

            var regionDTO = _mapper.Map<RegionDTO>(region);

            return CreatedAtAction(nameof(GetRegion), new { id = regionDTO.Id } ,regionDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegion(Guid id)
        {
            var region = await _regionRepository.DeleteAsync(id);

            if(region == null)
            {
                return NotFound();
            }

            var regionDTO = _mapper.Map<RegionDTO>(region);

            return Ok(regionDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegion(Guid id, UpdateRegionRequest updateRegionRequest)
        {
            var newRegion = new Region
            {
                Code = updateRegionRequest.Code,
                Name = updateRegionRequest.Name,
                Lat = updateRegionRequest.Lat,
                Population = updateRegionRequest.Population,
                Area = updateRegionRequest.Area,
                Long = updateRegionRequest.Long,
            };

            var regionFromDb = await _regionRepository.UpdateAsync(id, newRegion);

            if(regionFromDb == null)
            {
                return NotFound();
            }

            var regionDTO = _mapper.Map<RegionDTO>(regionFromDb);

            return Ok(regionDTO);
        }
    }
}
