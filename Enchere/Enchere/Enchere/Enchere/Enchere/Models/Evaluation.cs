using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Enchere.Models {
    public class Evaluation {
        public string Id { get; set; }
        public string IdEnchere { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime Date { get; set; }
        [Range(-3,3)]
        public int Cote { get; set; }
        [Display(Name = "De Membre")]
        public string IdMembreDe { get; set; }
        [Display(Name = "A Membre")]
        public string IdMembreA { get; set; }
        [Required]
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