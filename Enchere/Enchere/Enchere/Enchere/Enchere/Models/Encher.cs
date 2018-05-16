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
        [Required]
        public int Etat { get; set; }
        ///
        /// -1: prete a commecer; 0: EnCours; 1: Remporte; 2: Perdu; 3: EvalueAcheteur; 4: EvalueVendeur; 5: EvalueFinit
        /// 
        /// Lister tous les objets encherits:       EnCours(0)       Acheteur
        /// Lister tous les objets achetes:         Remporte(1)      Acheteur
        /// Lister tous les encheres vendus:        Remporte(1)      Vendeur
        /// Lister tous les encheres perdus:        Perdu(2)         Vendeur
        /// Lister tous les encheres en cours:      EnCours(0)       Vendeur



        public Encher() {}

        public Encher(string id, string idObjet, string idVendeur, string idAcheteur, decimal prixAchat, decimal pasDePrix, DateTime dateDepart, DateTime dateFin, int etat) {
            Id = id;
            IdObjet = idObjet;
            IdVendeur = idVendeur;
            IdAcheteur = idAcheteur;
            PrixAchat = prixAchat;
            PasDePrix = pasDePrix;
            DateDepart = dateDepart;
            DateFin = dateFin;
            Etat = etat;
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