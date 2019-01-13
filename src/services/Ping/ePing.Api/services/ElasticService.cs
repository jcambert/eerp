using AutoMapper;
using Elasticsearch.Net;
using ePing.Api.dbcontext;
using Microsoft.Extensions.Configuration;
using Nest;
using System;
using System.Net.Http;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using ePing.Api.models;

namespace ePing.Api.services
{

    public interface IElasticService
    {
        ElasticClient Elastic { get; }
    }
    public class ElasticService :  IElasticService
    {

        public ElasticService(IOptions< ElasticSettings> settings) 
        {
            List<Uri> nodes = new List<Uri>();
            settings.Value?.Nodes?.ToList().ForEach(cn =>
            {
                nodes.Add(new Uri(cn));
            });
           

            var pool = new StaticConnectionPool(nodes);
            var consettings = new ConnectionSettings(pool);
            consettings.DefaultIndex("ping");
            consettings.DefaultTypeName("_doc");
            consettings.DefaultMappingFor<Club>(c => c.IdProperty(p => p.Numero));
            consettings.DefaultMappingFor<Joueur>(c => c.IdProperty(p => p.Licence));
            Elastic = new ElasticClient(consettings);
        }

        public ElasticClient Elastic { get; }
    }
}
