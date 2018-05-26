using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Enchere.Models {
    public class EnchereViewModel {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Description { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        [Display(Name = "Date Inscrit")]
        public DateTime DateInscri { get; set; }
        [Required]
        [Display(Name = "Categorie")]
        public string IdCategorie { get; set; }
        public string Photo { get; set; }
        public string Piece { get; set; }
        public string IdMembre { get; set; }
        public bool Nouveau { get; set; }
        [Display(Name = "En Vente")]
        public bool EnVent { get; set; }
        [Required]
        [Display(Name = "Prix Depart ($)")]
        public decimal PrixDepart { get; set; }
        [Display(Name = "Prix Actuel ($)")]
        public decimal PrixActuel { get; set; }
        public string IdEnchere { get; set; }
        public int Etat { get; set; }

        public EnchereViewModel() { }

        public EnchereViewModel(string id, string nom, string description, DateTime dateInscri, string idCategorie, string photo, string piece, string idMembre, bool nouveau, bool enVent, decimal prixDepart, decimal prixActuel, string idEnchere, int etat) {
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
            PrixActuel = prixActuel;
            IdEnchere = idEnchere;
            Etat = etat;
        }
    }
}