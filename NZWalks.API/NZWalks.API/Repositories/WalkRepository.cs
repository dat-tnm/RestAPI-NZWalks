using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext _nZWalksDb;

        public WalkRepository(NZWalksDbContext nZWalksDb)
        {
            _nZWalksDb = nZWalksDb;
        }

        public async Task<Walk> AddAsync(Walk walk)
        {
            walk.Id = Guid.NewGuid();
            _nZWalksDb.Walks.Add(walk);
            await _nZWalksDb.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk> DeleteAsync(Guid id)
        {
            var walk = await _nZWalksDb.Walks.FirstOrDefaultAsync(walk => walk.Id == id);

            if (walk == null)
            {
                return null;
            }

            _nZWalksDb.Walks.Remove(walk);
            await _nZWalksDb.SaveChangesAsync();
            return walk;
        }

        public async Task<List<Walk>> GetAllAsync()
        {
            return await _nZWalksDb.Walks
                .Include(walk => walk.WalkDifficulty)
                .Include(walk => walk.Region)
                .ToListAsync();
        }

        public async Task<Walk> GetAsync(Guid id)
        {
            return await _nZWalksDb.Walks
                .Include(walk => walk.WalkDifficulty)
                .Include(walk => walk.Region)
                .FirstOrDefaultAsync(walk => walk.Id == id);
        }

        public async Task<Walk> UpdateAsync(Guid id, Walk updateWalkObj)
        {
            var walkFromDb = await _nZWalksDb.Walks.FirstOrDefaultAsync(walk => walk.Id == id);

            if (walkFromDb == null)
            {
                return null;
            }

            walkFromDb.WalkDifficultyId = updateWalkObj.WalkDifficultyId;
            walkFromDb.RegionId = updateWalkObj.RegionId;
            walkFromDb.Name = updateWalkObj.Name;
            walkFromDb.Length = updateWalkObj.Length;

            await _nZWalksDb.SaveChangesAsync();
            return walkFromDb;
        }
    }
}
