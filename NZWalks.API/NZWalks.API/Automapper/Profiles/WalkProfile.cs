using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Automapper.Profiles
{
    public class WalkProfile : Profile
    {
        public WalkProfile() 
        {
            CreateMap<Walk, WalkDTO>()
                .ReverseMap();

            CreateMap<WalkDifficulty, WalkDifficultyDTO>()
                .ReverseMap();
        }
    }
}
