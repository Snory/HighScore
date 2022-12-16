using AutoMapper;
using HighScore.Domain.Entities;
using HighScore.Domain.Models;

namespace HighScore.API.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserWriteDTO, UserEntity>();
            CreateMap<UserEntity, UserReadDTO>();
        }
    }
}
