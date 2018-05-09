
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Enchere.Ressource;

namespace Enchere.Models
{
    public class Membre
    {
        [Key]
        [Display(Name = "Numéro", ResourceType = typeof(ResourceView))]
        [Required(ErrorMessageResourceType = typeof(ResourceView),
            ErrorMessageResourceName = "entrerNr")]
        public string Numero { get; set; }

        [Display(Name = "Civilté", ResourceType = typeof(ResourceView))]
        public string Civilite { get; set; }

        [Display(Name = "Nom", ResourceType = typeof(ResourceView))]
        public string Nom { get; set; }

        [Display(Name = "Prenom", ResourceType = typeof(ResourceView))]
        public string Prenom { get; set; }

        [Display(Name = "Langue", ResourceType = typeof(ResourceView))]
        public string Langue { get; set; }

        // [RegularExpression("^[0-9]{3}[-. ]?[0-9]{3}[-. ]?[0-9]{4}$", ErrorMessageResourceType = typeof(ResourceView),
        //  ErrorMessageResourceName = "mauvaisformattel")]
        //  [RegularExpression("^[0-9]{3}[-. ]?[0-9]{3}[-. ]?[0-9]{4}$", ErrorMessage = "Ce n'est pas le bon format de telephone!")]
        [Required(ErrorMessageResourceType = typeof(ResourceView),
            ErrorMessageResourceName = "mauvaisformattel")]
        // [Required(ErrorMessage = "Ce n'est pas le bon format de telephone!")]
        [Display(Name = "Téléphone", ResourceType = typeof(ResourceView))]
        //[Display(Name = "Téléphone")]
        public string Telephone { get; set; }

        [Display(Name = "Adresse", ResourceType = typeof(ResourceView))]
        public string Adresse { get; set; }

        [Required(ErrorMessageResourceType = typeof(ResourceView),
             ErrorMessageResourceName = "entrerCourriel")]
        [Display(Name = "Courriel", ResourceType = typeof(ResourceView))]
        [DataType(DataType.EmailAddress)]
        public string Courriel { get; set; }

        [Display(Name = "Inscription", ResourceType = typeof(ResourceView))]
        public DateTime DateInscri { get; set; }

        [Display(Name = "Cote", ResourceType = typeof(ResourceView))]
        [Range(-3, 3, ErrorMessage = "Min: -3, max: 3")]
        public int Cote { get; set; }

        [Required(ErrorMessageResourceType = typeof(ResourceView),
           ErrorMessageResourceName = "MDPrequis"), StringLength(50, MinimumLength = 6,
           ErrorMessageResourceType = typeof(ResourceView),
           ErrorMessageResourceName = "MDPminmax")]
        // [Required(ErrorMessage = "MDPrequis"), StringLength(50, MinimumLength = 6, ErrorMessage = "Le mot de passe doit compter minimum 6 caractères et maximum 50!")]
        [DataType(DataType.Password)]
        [Display(Name = "MDP", ResourceType = typeof(ResourceView))]
        //[Display(Name = "Mot de passe")]
        public string MotDePasse { get; set; }


        [Required(ErrorMessageResourceType = typeof(ResourceView),
            ErrorMessageResourceName = "MDPrequis"), StringLength(50, MinimumLength = 6,
            ErrorMessageResourceType = typeof(ResourceView),
            ErrorMessageResourceName = "ConfirmezMDP")]
        // [Required(ErrorMessage = "MDPrequis"), StringLength(50, MinimumLength = 6, ErrorMessage = "Confirmez le mot de passe: minimum 6 caractères et maximum 50!")]
        [DataType(DataType.Password)]
        //[Display(Name = "Confirmez le mot de passse:")]
        [Compare("MotDePasse", ErrorMessageResourceType = typeof(ResourceView),
            ErrorMessageResourceName = "MDPCONFIRMDIFFERENT")]
        //  [Compare("MotDePasse", ErrorMessage = "Le mot de passe et la confirmation ne correspondent pas.")]
        [Display(Name = "Confirmation", ResourceType = typeof(ResourceView))]
        public string Confirmation { get; set; }

        public Membre() { }


        public Membre(string id, string civilite, string nom, string prenom, string langue, string telephone, string adresse, string courriel, DateTime dateInscri, int cote, string motDePasse)
        {
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