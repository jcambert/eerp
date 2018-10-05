using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ePing.Models
{
    [DataContract]
    public class UserDto
    {
        [DataMember(Name ="user")]
        [JsonProperty("user")]
        public  UserViewModel User { get; set; }

        
    }

    public class UserViewModel
    {
        [DataMember(Name ="id")]
        [JsonProperty("id")]
        public string Licence { get; set; }
        [DataMember(Name = "nom")]
        [JsonProperty("nom")]
        public string Nom { get; set; }
        [DataMember(Name = "prenom")]
        [JsonProperty("prenom")]
        public string Prenom { get; set; }
        [DataMember(Name = "numeroClub")]
        [JsonProperty("numeroClub")]
        public string NumeroClub { get; set; }
        [DataMember(Name = "nomClub")]
        [JsonProperty("nomClub")]
        public string  NomClub { get; set; }

    }
}
