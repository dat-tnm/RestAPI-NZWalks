using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly NZWalksDbContext _nZWalksDb;

        public WalkDifficultyRepository(NZWalksDbContext nZWalksDb)
        {
            _nZWalksDb = nZWalksDb;
        }

        public async Task<WalkDifficulty> AddAsync(WalkDifficulty difficulty)
        {
            difficulty.Id = Guid.NewGuid();
            _nZWalksDb.WalkDifficulties.Add(difficulty);
            await _nZWalksDb.SaveChangesAsync();
            return difficulty;
        }

        public async Task<WalkDifficulty> DeleteAsync(Guid id)
        {
            var difficultyFromDb = await _nZWalksDb.WalkDifficulties.FirstOrDefaultAsync(d => d.Id == id);

            if (difficultyFromDb == null)
            {
                return null;
            }

            _nZWalksDb.Remove(difficultyFromDb);
            await _nZWalksDb.SaveChangesAsync();
            return difficultyFromDb;
        }

        public async Task<List<WalkDifficulty>> GetAllAsync()
        {
            return await _nZWalksDb.WalkDifficulties.ToListAsync();
        }

        public async Task<WalkDifficulty> GetAsync(Guid id)
        {
            return await _nZWalksDb.WalkDifficulties.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<WalkDifficulty> UpdateAsync(Guid id, WalkDifficulty difficulty)
        {
            var difficultyFromDb = await _nZWalksDb.WalkDifficulties.FirstOrDefaultAsync(d => d.Id == id);

            if (difficultyFromDb == null)
            {
                return null;
            }

            difficultyFromDb.Code = difficulty.Code;
            await _nZWalksDb.SaveChangesAsync();
            return difficultyFromDb;
        }
    }
}