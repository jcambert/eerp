using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Intranet.Api.dto
{
    [DataContract()]
    public class ParametreDto
    {
        [DataMember(Name = "formats")]
        [JsonProperty("formats")]
        public List<FormatDto> Formats { get; set; }
        [DataMember(Name = "operations")]
        [JsonProperty("operations")]
        public List<OperationDto> Operations { get; set; }
        [DataMember(Name = "delais")]
        [JsonProperty("delais")]
        public List<DelaiDto> Delais{ get; set; }
        [DataMember(Name = "difficultes")]
        [JsonProperty("difficultes")]
        public List<DifficulteDto> Difficultes { get;  set; }
        [DataMember(Name = "traitements")]
        [JsonProperty("traitements")]
        public List<TraitementDto> Traitements { get; set; }
        [DataMember(Name = "typematieres")]
        [JsonProperty("typematieres")]
        public List<TypeMatiereDto> TypeMatieres { get;  set; }
        [DataMember(Name = "matieres")]
        [JsonProperty("matieres")]
        public List<MatiereDto> Matieres { get;  set; }
    }
}
