using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Enchere.Models {
    public class Membre {
        [Key]
        [Required(ErrorMessage = "Indiquez votre numero")]
        public string Numero { get; set; }

        // [Display(Name = "Nom de famille", ResourceType = typeof(ResourceView))]
        [Display(Name = "Civilté")]
        public string Civilite { get; set; }

        //  [Required(ErrorMessage = "Indiquez votre nom de famille")]
        // [Display(Name = "Nom de famille", ResourceType = typeof(ResourceView))]
        [Display(Name = "Nom de famille")]
        public string Nom { get; set; }

        //  [Required(ErrorMessage = "Indiquez votre prenom")]
        // [Display(Name = "Prenom", ResourceType = typeof(ResourceView))]
        public string Prenom { get; set; }

        // [Display(Name = "Langue", ResourceType = typeof(ResourceView))]
        [Display(Name = "Langue")]
        public string Langue { get; set; }

        //  [RegularExpression("^[0-9]{3}[-. ]?[0-9]{3}[-. ]?[0-9]{4}$", ErrorMessage = "Ce n'est pas le bon format de telephone!")]
        [Required(ErrorMessage = "Ce n'est pas le bon format de telephone!")]
        // [Display(Name = "Téléphone", ResourceType = typeof(ResourceView))]       
        [Display(Name = "Téléphone")]
        public string Telephone { get; set; }

        //  [Required(ErrorMessage = "Indiquez votre adresse")]
        // [Display(Name = "Adresse", ResourceType = typeof(ResourceView))]
        [Display(Name = "Adresse")]
        public string Adresse { get; set; }

        [Required(ErrorMessage = "Indiquez votre courriel")]
        //  [Display(Name = "Adresse courriel", ResourceType = typeof(ResourceView))]
        [Display(Name = "Adresse courriel")]
        [DataType(DataType.EmailAddress)]
        public string Courriel { get; set; }


        public DateTime DateInscri { get; set; }
        //  public string DateInscri { get; set; }

        //  [Required(ErrorMessage = "Indiquez la cote")]
        //  [Display(Name = "Cote", ResourceType = typeof(ResourceView))]
        [Range(-3, 3, ErrorMessage = "Min: -3, max: 3")]
        [Display(Name = "Cote")]
        public int Cote { get; set; }

        [Required(ErrorMessage = " Le mot de passe est requis "), StringLength(50, MinimumLength = 6, ErrorMessage = "Le mot de passe doit compter minimum 6 caractères et maximum 50!")]
        [DataType(DataType.Password)]
        // [Display(Name = "Mot de passe", ResourceType = typeof(ResourceView))]
        [Display(Name = "Mot de passe")]
        public string MotDePasse { get; set; }

        [Required(ErrorMessage = " Le mot de passe est requis "), StringLength(50, MinimumLength = 6, ErrorMessage = "Confirmez le mot de passe: minimum 6 caractères et maximum 50!")]
        [DataType(DataType.Password)]
        //[Display(Name = "Confirmez le mot de passse:")]
        [Compare("MotDePasse", ErrorMessage = "Le mot de passe et la confirmation ne correspondent pas.")]
        public string Confirmation { get; set; }

        public Membre() { }


        public Membre(string id, string civilite, string nom, string prenom, string langue, string telephone, string adresse, string courriel, DateTime dateInscri, int cote, string motDePasse) {
            Numero = id;
            Civilite = civilite;
            Nom = nom;
            Prenom = prenom;
            Langue = langue;
            Telephone = telephone;
            Adresse = adresse;
            Courriel = courriel;
            DateInscri = dateInscri;
            Cote = cote;
            MotDePasse = motDePasse;
        }
    }
}