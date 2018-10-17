using AutoMapper;
using ePing.Api.dto;
using ePing.Api.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ePing.Api
{
    public class DomainProfile:Profile
    {
        public DomainProfile()
        {
            CreateMap<ClubDto, Club>().ReverseMap();
            CreateMap<JoueurSpidDto, JoueurSpid>().ReverseMap();
            CreateMap<PartieDto, Partie>();
            CreateMap<PartieHistoDto, Historique>();
            CreateMap<ClassementDto, Classement>();
            CreateMap<Journee, HistoriquePointDto>().ConvertUsing(journee =>
            {
                return new HistoriquePointDto() { Date = journee.Date, PointsGagnesPerdus = journee.PointsGagnesPerdus };
            });
        }
    }
}
