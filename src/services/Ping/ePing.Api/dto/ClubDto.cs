using System.Runtime.Serialization;

namespace ePing.Api.dto
{

    [DataContract()]
    public class ListeClubHeader
    {
        [DataMember(Name = "liste")]
        public ClubDtoHeader Liste { get; set; }

    }
    
    
    public class ClubDtoHeader
    {
        [DataMember(Name = "club")]
        public ClubDto Club { get; set; }

    }

    public class ClubDto
    {
        [DataMember(Name = "idclub")]
        public string idClub { get; set; }
        [DataMember(Name = "numero")]
        public string Numero { get; set; }
        [DataMember(Name = "nom")]
        public string Nom { get; set; }
        [DataMember(Name = "validation")]
        public string Validation { get; set; }
    }
}
