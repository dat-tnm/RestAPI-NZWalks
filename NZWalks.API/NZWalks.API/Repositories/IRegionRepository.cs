using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        public Task<IEnumerable<Region>> GetAllAsync();

        public Task<Region> GetAsync(Guid id);

        public Task<Region> AddAsync(Region region);

        public Task<Region> DeleteAsync(Guid id);

        public Task<Region> UpdateAsync(Guid id, Region region);
    }
}
