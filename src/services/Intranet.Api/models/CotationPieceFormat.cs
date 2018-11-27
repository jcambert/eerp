using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Intranet.Api.models
{
    public class CotationPieceFormat:IdTrackable
    {
        [Range(0,int.MaxValue)]
        public int Longueur { get; set; }
        [Range(0, int.MaxValue)]
        public int Largeur { get; set; }
        public bool Disponible { get; set; } = true;
        public bool Selection { get; set; } = false;

        #region Navigation / Relationship
        public CotationPiece Piece { get; set; }
        #endregion
    }
}
