using Auth.Api.dto;
using Auth.Api.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Api
{
    public class DomainProfile:Profile
    {
        public DomainProfile()
        {
            CreateMap<JoueurDto, PingUser>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Licence))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => $"{src.Nom} {src.Prenom}"))
                .ForMember(dest => dest.Nom, opt => opt.MapFrom(src => src.Nom))
                .ForMember(dest => dest.Prenom, opt => opt.MapFrom(src => src.Prenom))
                .ForMember(dest => dest.NumeroClub, opt => opt.MapFrom(src => src.NumeroClub??src.Club))
                .ForMember(dest => dest.NomClub, opt => opt.MapFrom(src => src.NomClub??src.NClub))
                .ForMember(dest => dest.Sexe, opt => opt.MapFrom(src => src.Sexe))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Certificat, opt => opt.MapFrom(src => src.Certficat))
                .ForMember(dest => dest.Validation, opt => opt.MapFrom(src => src.Validation))
                .ForMember(dest => dest.Echelon, opt => opt.MapFrom(src => src.Echelon))
                .ForMember(dest => dest.Place, opt => opt.MapFrom(src => src.Place))
                .ForMember(dest => dest.Point, opt => opt.MapFrom(src => src.Point))
                .ForMember(dest => dest.Categorie, opt => opt.MapFrom(src => src.Categorie))

                ;
            CreateMap<LoginResultViewModel, LoginResult>().ReverseMap();

           
        }
    }
}
