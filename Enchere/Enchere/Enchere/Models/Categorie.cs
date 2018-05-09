using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enchere.Models {
    public class Categorie {
        public string Id { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }

        public Categorie(string id, string nom, string description) {
            Id = id;
            Nom = nom;
            Description = description;
        }
    }
}