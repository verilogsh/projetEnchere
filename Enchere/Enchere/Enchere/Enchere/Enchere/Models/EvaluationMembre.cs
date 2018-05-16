using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enchere.Models
{
    public class EvaluationMembre
    {
        public string  Nom { get; set; }
        public String NumMembre { get; set; }
        public int Cote { get; set; }
        public int NbrEvaluation { get; set; }

        public EvaluationMembre() { }

        public EvaluationMembre(String numMembre,string nom,int cote,int nbrEvaluation)
        {
            this.Nom = nom; 
            this.NumMembre = numMembre;
            this.Cote = cote;
            this.NbrEvaluation = nbrEvaluation;

        }
    }
}