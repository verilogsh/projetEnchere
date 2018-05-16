using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Enchere.Ressource;

namespace Enchere.Models {
    public class Categorie {
        [Key]
        [Display(Name = "Numéro", ResourceType = typeof(ResourceView))]
        public string Id { get; set; }

        [Display(Name = "nom2", ResourceType = typeof(ResourceView))]
        public string Nom { get; set; }

        public string Description { get; set; }

        public Categorie() { }

        public Categorie(string id, string nom, string description) {
            Id = id;
            Nom = nom;
            Description = description;
        }
    }
}