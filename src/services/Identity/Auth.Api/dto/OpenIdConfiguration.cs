using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Auth.Api.dto
{
    [DataContract]
    public class OpenIdConfiguration
    {
        [DataMember(Name ="issuer")]
        public string Issuer{ get; set; }
        [DataMember(Name ="token_endpoint")]
        public string TokenEndPoint { get; set; }
    }
}
