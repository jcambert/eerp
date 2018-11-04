using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ePing.Api.dto
{
    [DataContract]
    public class ListeOrganismesHeader
    {
        [DataMember(Name = "liste")]
        [JsonProperty("liste")]
        public OrganismesDtoHeader Liste { get; set; }
    }


    public class ListeOrganismeHeader
    {
        [DataMember(Name = "liste")]
        [JsonProperty("liste")]
        public OrganismeDtoHeader Liste { get; set; }
    }

    public class OrganismeDtoHeader
    {


        [DataMember(Name = "organisme")]
        [JsonProperty("organisme")]
        public OrganismeDto Organisme { get; set; }

    }
    public class OrganismesDtoHeader
    {


        [DataMember(Name = "organisme")]
        [JsonProperty("organisme")]
        public List<OrganismeDto> Organismes { get; set; }

        public OrganismeDto Organisme { get; set; }

    }
    public class OrganismeDto
    {
        [DataMember(Name = "id")]
        [JsonProperty("id")]
        public string Identifiant { get; set; }
        [DataMember(Name = "libelle")]
        [JsonProperty("libelle")]
        public string Libelle { get; set; }
        [DataMember(Name = "code")]
        [JsonProperty("code")]
        public string Code { get; set; }
        [DataMember(Name = "idpere")]
        [JsonProperty("idpere")]
        public string IdPere { get; set; }
    }
}
