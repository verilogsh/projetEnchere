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
        [GreatThan("PrixAchat")]
        public decimal PrixAchat { get; set; }
        [Required]
        [GreatThan("PasDePrix")]
        public decimal PasDePrix { get; set; }
        [Required]
        [LaterThan("DateDepart")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime DateDepart { get; set; }
        [Required]
        [LaterThanOther("DateFin")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
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



        public Encher() { }

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

        public class GreatThanAttribute : ValidationAttribute {
            string attri { get; set; }
            public GreatThanAttribute(string attri) {
                this.attri = attri;
            }

            protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
                var property = validationContext.ObjectType.GetProperty(attri);
                if (property == null) {
                    return new ValidationResult(
                        string.Format("Unknown property: {0}", attri)
                    );
                }

                if (attri == "PrixAchat" && (decimal)value <= 0 || attri == "PasDePrix" && (decimal)value <= 0) {
                    return new ValidationResult(validationContext.DisplayName + " ne peut pas equal ou moins que zero!");
                }

                return null;
            }
        }

        public class LaterThanAttribute : ValidationAttribute {
            string attri { get; set; }
            public LaterThanAttribute(string attri) {
                this.attri = attri;
            }

            protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
                var property = validationContext.ObjectType.GetProperty(attri);
                if (property == null) {
                    return new ValidationResult(
                        string.Format("Unknown property: {0}", attri)
                    );
                }

                DateTime dt = Convert.ToDateTime(value);

                if (attri == "DateDepart" && dt.CompareTo(DateTime.Now) <= 0) {
                    return new ValidationResult("Date depart ne peut pas etre plus tot qu'aujourd'hui!");
                }

                var por = validationContext.ObjectInstance.GetType().GetProperty("DateFin");
                DateTime dt1 = Convert.ToDateTime(por.GetValue(validationContext.ObjectInstance, null));

                return null;
            }
        }

        public class LaterThanOtherAttribute : ValidationAttribute {
            string attri { get; set; }
            public LaterThanOtherAttribute(string attri) {
                this.attri = attri;
            }

            protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
                var property = validationContext.ObjectType.GetProperty(attri);
                if (property == null) {
                    return new ValidationResult(
                        string.Format("Unknown property: {0}", attri)
                    );
                }

                DateTime dt = Convert.ToDateTime(value);
                var por = validationContext.ObjectInstance.GetType().GetProperty("DateDepart");
                DateTime dt1 = Convert.ToDateTime(por.GetValue(validationContext.ObjectInstance, null));
                if (attri == "DateFin" && dt.CompareTo(dt1) <= 0) {
                    return new ValidationResult("Date de fin ne peut pas etre plus tot que la date depart!");
                }
                return null;
            }
        }
    }
}