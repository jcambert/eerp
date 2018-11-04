﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ePing.Api.models
{
    public class ResultatRencontre:Trackable
    {
        public string Libelle { get; set; }

        public string EquipeA { get; set; }

        public string EquipeB { get; set; }

        public int? ScoreA { get; set; }

        public int? ScoreB { get; set; }

        public string LienRencontre { get; set; }

        public string DatePrevue { get; set; }

        public string DateReelle { get; set; }
    }
}
