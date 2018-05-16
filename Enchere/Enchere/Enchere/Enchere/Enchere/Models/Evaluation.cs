using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enchere.Models {
    public class Evaluation {
        public string Id { get; set; }
        public string IdEnchere { get; set; }
        public DateTime Date { get; set; }
        public int Cote { get; set; }
        public string IdMembreDe { get; set; }
        public string IdMembreA { get; set; }
        public string Commentaire { get; set; }

        public Evaluation(string id, string idEnchere, DateTime date, int cote, string commentaire, string idMembreDe, string idMembreA) {
            Id = id;
            IdEnchere = idEnchere;
            Date = date;
            Cote = cote;
            IdMembreDe = idMembreDe;
            IdMembreA = idMembreA;
            Commentaire = commentaire;
        }

        public Evaluation() {}
    }
}