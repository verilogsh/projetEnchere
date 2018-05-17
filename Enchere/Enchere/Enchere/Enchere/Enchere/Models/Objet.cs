using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Enchere.Models {
    public class Objet {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Description { get; set; }
        [Display(Name ="Date Inscrit"),DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime DateInscri { get; set; }
        [Required]
        [Display(Name ="Categorie")]
        public string IdCategorie { get; set; }
        public string Photo { get; set; }
        public string Piece { get; set; }
        public string IdMembre { get; set; }
        public bool Nouveau { get; set; }
        [Display(Name = "En Vente")]
        public bool EnVent { get; set; }
        [Required]
        public decimal PrixDepart { get; set; }

        public Objet() {  }

        public Objet(string id, string nom, string description, DateTime dateInscri, string idCategorie, string photo, string piece, string idMembre, bool nouveau, bool enVent, decimal prixDepart) {
            Id = id;
            Nom = nom;
            Description = description;
            DateInscri = dateInscri;
            IdCategorie = idCategorie;
            Photo = photo;
            Piece = piece;
            IdMembre = idMembre;
            Nouveau = nouveau;
            EnVent = enVent;
            PrixDepart = prixDepart;
        }
    }
}