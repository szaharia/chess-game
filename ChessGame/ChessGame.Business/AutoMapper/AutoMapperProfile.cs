using AutoMapper;
using ChessGame.Business.Contracts.Models;

namespace ChessGame.Business.AutoMapper
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Data.Contracts.Entities.Player, Player>().ReverseMap();
            CreateMap<Data.Contracts.Entities.Game, Game>().ReverseMap();
        }
    }
}
