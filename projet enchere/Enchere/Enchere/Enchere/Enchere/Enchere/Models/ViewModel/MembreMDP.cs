using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Enchere.Models.ViewModel {
    public class MembreMDP {
       
        public string IdMembreMDP { get; set; }

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

        public MembreMDP() { }

        //public MembreMDP(int id, string mdp, string confirm) {
        //    IdMembreMDP = id;
        //    MotDePasse = mdp;
        //    Confirmation = confirm;
        //}

        public MembreMDP(Membre u) {
            IdMembreMDP = u.Numero;
            MotDePasse = u.MotDePasse;
            Confirmation = u.Confirmation;
        }
    }
}