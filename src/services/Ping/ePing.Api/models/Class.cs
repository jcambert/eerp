using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ePing.Api.models
{
    public class Class
    {
        public Class()
        {
            var rencontre = new Rencontre();

            var rg = new RegleRencontre() { Id = 0, Description = "Chpt France Par equipe Masc.", Division = 1, Epreuve = 2 };
            rg.Regles.Add(item: new Regle() {Id=0,Description="Rencontre dispute score acquis",Obligatoire=true,Type=RegleType.OuiNon,Defaut="oui",Champs= "ScoreAcquis",MessageErreur="La rencontre doit etre " });

            List <RegleRencontreValidation> results;
            rg.Validate(rencontre, out results);
        }
    }
}
