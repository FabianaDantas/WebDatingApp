using System;
using AutoMapper;
using WebDatingApp.API.Domain.Models;
using WebDatingApp.API.Domain.DTOs;
using System.Linq;

namespace WebDatingApp.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListDTO>()
                .ForMember(destino => destino.PhotoUrl, opt => opt.MapFrom(source => 
                            source.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ForMember(destino => destino.Age, opt => opt.MapFrom(src => 
                            src.DateOfBirth.CalculateAge()));

            CreateMap<User, UserForDetailedDTO>()
                .ForMember(destino => destino.PhotoUrl, opt => opt.MapFrom(source => 
                            source.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ForMember(destino => destino.Age, opt => opt.MapFrom(src => 
                            src.DateOfBirth.CalculateAge()));

            CreateMap<Photo, PhotosForDetailedDTO>();
            CreateMap<UserForUpdateDTO,User>();
            CreateMap<UserRegisterDTO,User>();
            CreateMap<Photo, PhotoForReturnDTO>();
            CreateMap<PhotoForCreationDTO, Photo>();
        }
    }
} 