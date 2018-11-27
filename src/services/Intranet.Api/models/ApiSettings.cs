using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intranet.Api.models
{
    public  class ApiSettings
    {
        public string TypeMatiere { get; set; }
        public string Matiere { get;  set; }
        public string Difficulte { get;  set; }
        public string Delai { get;  set; }
        public string TraitementDeSurface { get;  set; }
        public CodePrimaire CodePrimaire { get; set; }
        public string Format { get;  set; }
        public string Operation { get;  set; }
    }

    public class CodePrimaire
    {
        public int TypeMatiere { get; set; }
        public int Matiere { get; set; }
        public int Difficulte { get; set; }
        public int Delai { get; set; }
        public int TraitementDeSurface { get; set; }
        public int Format { get;  set; }
        public int Operation { get;  set; }
    }

}
