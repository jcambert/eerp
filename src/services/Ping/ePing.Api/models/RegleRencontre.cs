using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace ePing.Api.models
{
    internal static class RegleRencontreExtensions
    {
        internal static bool Validate(this RegleRencontre regle,Rencontre rencontre, out List<RegleRencontreValidation> validations)
        {
            validations = new List<RegleRencontreValidation>();
            foreach (var r in regle.Regles)
            {
                validations.AddRange(r.Validate(rencontre));
            }
            
            return true;
        }

        internal static List<RegleRencontreValidation>  Validate(this Regle regle,Rencontre rencontre)
        {
            var props=typeof(Rencontre).GetProperties().Where(p => p.Name == regle.Champs).FirstOrDefault();
            var value=props.GetGetMethod().Invoke(rencontre, new object[] { });
            return null;
        }
    }

    public class RegleRencontre:Trackable
    {
        [Key]
        public int Id { get; set; }

        public string Description { get; set; }

        public int Epreuve { get; set; }

        public int Division { get; set; }

        public List<Regle> Regles { get; set; } = new List<Regle>();
    }
}
