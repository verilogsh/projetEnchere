using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Enchere.Models {
    public class Encher {
        public string Id { get; set; }
        public string IdObjet { get; set; }
        public string IdVendeur { get; set; }
        public string IdAcheteur { get; set; }
        [Required]
        public decimal PrixAchat { get; set; }
        [Required]
        public decimal PasDePrix { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime DateDepart { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime DateFin { get; set; }



        public Encher() {}

        public Encher(string id, string idObjet, string idVendeur, string idAcheteur, decimal prixAchat, decimal pasDePrix, DateTime dateDepart, DateTime dateFin) {
            Id = id;
            IdObjet = idObjet;
            IdVendeur = idVendeur;
            IdAcheteur = idAcheteur;
            PrixAchat = prixAchat;
            PasDePrix = pasDePrix;
            DateDepart = dateDepart;
            DateFin = dateFin;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {
            if (DateDepart >= DateFin) {
                yield return
                  new ValidationResult(errorMessage: "EndDate must be greater than StartDate",
                                       memberNames: new[] { "EndDate" });
            }
        }
    }
}