﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Intranet.Api.dto
{
    [DataContract()]
    public class DifficulteDto
    {
        [DataMember(Name = "difficulte")]
        [JsonProperty("difficulte")]
        public string Difficulte { get; set; }
    }
}
