using AutoMapper;
using Intranet.Api.dbcontext;
using Intranet.Api.models;
using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace Intranet.Api.Seedings
{

    public interface ISeeder
    {
        void Seed(IntranetDbContext ctx);
    }
    public class Seeder : ISeeder
    {
        public Seeder(IOptions<ApiSettings> settings, IMapper mapper)
        {
            Settings = settings.Value;
            Mapper = mapper;
        }
        public ApiSettings Settings { get; }
        public IMapper Mapper { get; }

        public void Seed(IntranetDbContext ctx)
        {
            _Seed(ctx, TypeExtensions.Implement<ISeed>().ToArray());
        }

        internal void _Seed(IntranetDbContext ctx, params Type[] seeders)
        {
            foreach (var seeder in seeders)
            {
                ((ISeed)Activator.CreateInstance(seeder, ctx, Settings, Mapper))?.Seed();
            }
        }
    }
}
