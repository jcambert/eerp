﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ePing.Api.dto
{
    public class HistoriquePointDto
    {
        public DateTime Date { get; set; }
        public double PointsGagnesPerdus { get; set; } = 0;
    }
}
