using AutoMapper;
using Intranet.Api.dto;
using Intranet.Api.models;
using Newtonsoft.Json;
using System;
using System.Linq.Expressions;

namespace Intranet.Api
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            Func<Parametre, CotationPieceFormat> paramToPieceFormat = p =>
            {
                if (p.CodeSecondaire == 0) return null;
                FormatDto fmt= JsonConvert.DeserializeObject<FormatDto>(p.Designation);
                return new CotationPieceFormat()
                {
                    Longueur = fmt?.Longueur ?? 0,
                    Largeur = fmt?.Largeur ?? 0

                };
            };

            Func<Parametre, FormatDto> paramToFormat = p =>
             {
                 return JsonConvert.DeserializeObject<FormatDto>(p.Designation);
             };

            Func<FormatDto, string> formatToString = p =>
             {
                 return JsonConvert.SerializeObject(p);
             };


            Func<Parametre, OperationDto> paramToOperation = p =>
            {
               
               return JsonConvert.DeserializeObject<OperationDto>(p.Designation);
                
            };
            Func<OperationDto, string> operationToString = p =>
            {
                return JsonConvert.SerializeObject(p);
            };

            Func<Parametre, DelaiDto> paramToDelai = p =>
            {

                return JsonConvert.DeserializeObject<DelaiDto>(p.Designation);

            };
            Func<DelaiDto, string> delaiToString = p =>
            {
                return JsonConvert.SerializeObject(p);
            };

            Func<Parametre, DifficulteDto> paramToDifficulte= p =>
            {
                return JsonConvert.DeserializeObject<DifficulteDto>(p.Designation);
            };
            Func<DifficulteDto, string> difficulteToString = p =>
            {
                return JsonConvert.SerializeObject(p);
            };
            Func<Parametre, TraitementDto> paramToTraitement = p =>
            {
                return JsonConvert.DeserializeObject<TraitementDto>(p.Designation);
            };
            Func<TraitementDto, string> traitementToString = p =>
            {
                return JsonConvert.SerializeObject(p);
            };
            Func<Parametre, TypeMatiereDto> paramToTypeMatiere = p =>
            {
                return JsonConvert.DeserializeObject<TypeMatiereDto>(p.Designation);
            };
            Func<TypeMatiereDto, string> typeMatiereToString = p =>
            {
                return JsonConvert.SerializeObject(p);
            };
            Func<Parametre, MatiereDto> paramToMatiere = p =>
            {
                return JsonConvert.DeserializeObject<MatiereDto>(p.Designation);
            };
            Func<MatiereDto, string> matiereToString = p =>
            {
                return JsonConvert.SerializeObject(p);
            };

            CreateMap<Parametre, CotationPieceFormat>().ConstructUsing(FuncToExpression(paramToPieceFormat));

            CreateMap<Parametre, FormatDto>().ConstructUsing(FuncToExpression(paramToFormat));
            CreateMap<FormatDto, string>().ConstructUsing(FuncToExpression(formatToString));

            CreateMap<Parametre, OperationDto>().ConstructUsing(FuncToExpression(paramToOperation));
            CreateMap<OperationDto, string>().ConstructUsing(FuncToExpression(operationToString));

            CreateMap<Parametre, DelaiDto>().ConstructUsing(FuncToExpression(paramToDelai));
            CreateMap<DelaiDto, string>().ConstructUsing(FuncToExpression(delaiToString));

            CreateMap<Parametre, DifficulteDto>().ConstructUsing(FuncToExpression(paramToDifficulte));
            CreateMap<DifficulteDto, string>().ConstructUsing(FuncToExpression(difficulteToString));

            CreateMap<Parametre, TraitementDto>().ConstructUsing(FuncToExpression(paramToTraitement));
            CreateMap<TraitementDto, string>().ConstructUsing(FuncToExpression(traitementToString));

            CreateMap<Parametre, TypeMatiereDto>().ConstructUsing(FuncToExpression(paramToTypeMatiere));
            CreateMap<TypeMatiereDto, string>().ConstructUsing(FuncToExpression(typeMatiereToString));

            CreateMap<Parametre, MatiereDto>().ConstructUsing(FuncToExpression(paramToMatiere));
            CreateMap<MatiereDto, string>().ConstructUsing(FuncToExpression(matiereToString));
        }


        private static Expression<Func<T, TOut>> FuncToExpression<T,TOut>(Func<T, TOut> f)
        {
            return x => f(x);
        }
    }
}




