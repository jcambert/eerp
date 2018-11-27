using AutoMapper;
using Intranet.Api.dbcontext;
using Intranet.Api.dto;
using Intranet.Api.models;
using Microsoft.EntityFrameworkCore;

namespace Intranet.Api.Seedings
{
    public interface ISeed
    {
          void Seed();
    }

    internal abstract class BaseSeed<TModel>:ISeed where TModel : IdTrackable
    {
        public BaseSeed(IntranetDbContext ctx, ApiSettings settings,IMapper mapper)
        {
            Context = ctx;
            Settings = settings;
            Mapper = mapper;

        }

        public IntranetDbContext Context { get; }
        public ApiSettings Settings { get; }
        public IMapper Mapper { get; }

        public abstract void Seed();

        public DbSet<TModel> Set => Context.Set<TModel>();

    }

    internal abstract class ParametreSeed : BaseSeed<Parametre>
    {
        public ParametreSeed(IntranetDbContext ctx, ApiSettings settings,IMapper mapper) : base(ctx, settings,mapper)
        {
        }
        public abstract string Type { get; }
        public abstract int CodePrimaire { get; }
        public void Add(int codePrimaire = 0, int codeSecondaire = 0, string designation = "", int intValue = 0, double doubleValue = 0.0, string remarque = "", bool estValide = true)
        {
            Set.Add(new Parametre() { CodePrimaire = (CodePrimaire + codePrimaire), CodeSecondaire = codeSecondaire, Type = Type, Designation = designation, IntValue = intValue, DoubleValue = doubleValue, Remarque = remarque, EstValide = estValide });
        }
        public override void Seed()
        {
            Add(0, 0, Type);
        }
    }
    internal class TypeMatiereSeed : ParametreSeed
    {
        public TypeMatiereSeed(IntranetDbContext ctx, ApiSettings settings,IMapper mapper) : base(ctx, settings,mapper)
        {
        }

        public override string Type => Settings.TypeMatiere;

        public override int CodePrimaire => Settings.CodePrimaire.TypeMatiere;

        public override void Seed()
        {
           
            Add(0, 0, Mapper.Map<string>(new TypeMatiereDto() { TypeMatiere = "Acier",Densite=8.0 }));
            Add(1, 0, Mapper.Map<string>(new TypeMatiereDto() { TypeMatiere = "Inox", Densite = 8.0 }));
            Add(2, 0, Mapper.Map<string>(new TypeMatiereDto() { TypeMatiere = "Alu", Densite = 2.8 }));
        }
    }
}
