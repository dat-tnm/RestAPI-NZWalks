using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkDifficultyRepository
    {
        public Task<WalkDifficulty> GetAsync(Guid id);

        public Task<List<WalkDifficulty>> GetAllAsync();

        public Task<WalkDifficulty> DeleteAsync(Guid id);

        public Task<WalkDifficulty> UpdateAsync(Guid id, WalkDifficulty difficulty);

        public Task<WalkDifficulty> AddAsync(WalkDifficulty difficulty);
    }
}
