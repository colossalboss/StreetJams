using System;
using AutoMapper;
using StreetJams.Entities;
using StreetJamsAPI.ViewModels;

namespace StreetJamsAPI.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SongDto, Song>()
                .ForMember(dest => dest.SongUrl,
                    act => act.MapFrom(src => $"https://localhost:5001/{src.Title.FileName}"));
        }
    }
}
