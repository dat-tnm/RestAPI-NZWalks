using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
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
        public async Task<IActionResult> GetAllRegion()
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
    }
}
