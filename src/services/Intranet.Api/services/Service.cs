using AutoMapper;
using Intranet.Api.dbcontext;
using Intranet.Api.dto;
using Intranet.Api.models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intranet.Api.services
{
    public class BaseService<TEntity>
        where TEntity : IdTrackable
    {
        public BaseService(IRepository<IntranetDbContext, TEntity> repository, IOptions<ApiSettings> settings,IMapper mapper)
        {
            Repository = repository;
            Settings = settings.Value;
            Mapper = mapper;
        }
        public IRepository<IntranetDbContext, TEntity> Repository { get; }
        public ApiSettings Settings { get; }
        public IMapper Mapper { get; }
        public IntranetDbContext Context => Repository.Context;
    }

    public class ParametreService : BaseService<Parametre>
    {
        public ParametreService(IRepository<IntranetDbContext, Parametre> repository, IOptions<ApiSettings> settings, IMapper mapper) : base(repository, settings, mapper)
        {
        }

        public ParametreDto Flatten()
        {
            ParametreDto result = new ParametreDto() {
                Formats = GetFormats(),
                Operations = GetOperations(),
                Delais = GetDelais(),
                Difficultes = GetDifficultes(),
                Traitements = GetTraitements(),
                TypeMatieres = GetTypeMatieres(),
                Matieres = GetMatieres(),
            };
            return result;
        }
        private List<TModel> GetModelMinMax<TModel>(int codePrimaireMin, int codePrimaireMax=99,int codeSecondaireMax=99)
        {
            return Mapper.Map<List<TModel>>(Repository.Get(p => p.CodePrimaire >= codePrimaireMin && p.CodePrimaire<= (codePrimaireMin+codePrimaireMax) && p.CodeSecondaire<=codeSecondaireMax).ToList());
        }
        private List<TModel> GetModel<TModel>(int codePrimaire)
        {
            return Mapper.Map<List<TModel>>(Repository.Get(p => p.CodePrimaire == codePrimaire && p.CodeSecondaire > 0).ToList());
        }

        public List<FormatDto> GetFormats()
        {
            return GetModel<FormatDto>(Settings.CodePrimaire.Format);
        }

        public List<OperationDto> GetOperations()
        {
            return GetModel<OperationDto>(Settings.CodePrimaire.Operation);
        }
        public List<DelaiDto> GetDelais()
        {
            return GetModel<DelaiDto>(Settings.CodePrimaire.Delai);
        }
        public List<DifficulteDto> GetDifficultes()
        {
            return GetModel<DifficulteDto>(Settings.CodePrimaire.Difficulte);
        }
        public List<TraitementDto> GetTraitements()
        {
            return GetModel<TraitementDto>(Settings.CodePrimaire.TraitementDeSurface);
        }
        public List<MatiereDto> GetMatieres()
        {
            return GetModelMinMax<MatiereDto>(Settings.CodePrimaire.Matiere,99,99).Where(m=>m.Matiere!=null).ToList();
        }
        public List<TypeMatiereDto> GetTypeMatieres()
        {
            return GetModelMinMax<TypeMatiereDto>(Settings.CodePrimaire.TypeMatiere,99,0);
        }
        public IQueryable<Parametre> GetByType(string type)
        {
            string _type = Settings.FindByTypeName(type);
            var parametres = Context.Parametres.Where(p => p.Type == _type);
            return parametres;
        }

        public IQueryable<Parametre> GetByCode(int primaire, int? secondaire)
        {
            var parametre = Context.Parametres.Where(p => p.CodePrimaire == primaire);
            if (secondaire != null)
                parametre = parametre.Where(p => p.CodeSecondaire == secondaire);
            return parametre;
        }
    }
}
