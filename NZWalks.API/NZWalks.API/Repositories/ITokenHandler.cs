using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface ITokenHandler
    {
        public Task<string> CreateTokenAsync(User user);
    }
}
