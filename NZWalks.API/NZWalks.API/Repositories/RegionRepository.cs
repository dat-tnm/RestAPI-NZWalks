using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext _nZWalksDb;

        public RegionRepository(NZWalksDbContext nZWalksDb)
        {
            _nZWalksDb = nZWalksDb;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await _nZWalksDb.Regions.ToListAsync();
        }
    }
}
