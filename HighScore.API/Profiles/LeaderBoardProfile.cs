using AutoMapper;
using HighScore.Domain.Entities;
using HighScore.Domain.Models;

namespace HighScore.API.Profiles
{
    public class LeaderBoardProfile : Profile
    {
        public LeaderBoardProfile()
        {
            CreateMap<LeaderBoardWriteDTO, LeaderBoardEntity>();
            CreateMap<LeaderBoardEntity, LeaderBoardDTO>();
        }
    }
}
