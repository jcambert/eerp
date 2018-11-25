using System.ComponentModel.DataAnnotations;

namespace ePing.Api.models
{

    public enum RegleType
    {
        OuiNon,
        Valeur,
    }
    public class Regle:Trackable
    {

        [Key]
        public int Id { get; set; }

        public string Description { get; set; }

        public bool Obligatoire { get; set; }

        public RegleType Type { get; set; }

        public string Defaut { get;  set; }
        public string Champs { get; internal set; }
        public string MessageErreur { get; internal set; }
    }
}