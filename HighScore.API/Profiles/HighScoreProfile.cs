using AutoMapper;
using HighScore.Domain.Entities;
using HighScore.Domain.Models;

namespace HighScore.API.Profiles
{
    public class HighScoreProfile : Profile
    {
        public HighScoreProfile()
        {
            CreateMap<HighScoreWriteDataDTO, HighScoreEntity>();
            CreateMap<HighScoreEntity, HighScoreWriteDataDTO>();
            CreateMap<HighScoreDTO, HighScoreEntity>();
            CreateMap<HighScoreEntity, HighScoreDTO>();
        }
    }
}
