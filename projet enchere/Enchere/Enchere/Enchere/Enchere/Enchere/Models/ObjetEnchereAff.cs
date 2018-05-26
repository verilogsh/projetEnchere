using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enchere.Models {
    public class ObjetEnchereAff {
        public ObjetEnchereAff(string id, string idEnchere, string nom, string description, string idCategorie, string photo, string piece, string idVendeur, string idAcheteur, decimal prixDepart, decimal prixActuel, DateTime dateDepart, DateTime dateFin, decimal pasDePrix) {
            Id = id;
            IdEnchere = idEnchere;
            Nom = nom;
            Description = description;
            IdCategorie = idCategorie;
            Photo = photo;
            Piece = piece;
            IdVendeur = idVendeur;
            IdAcheteur = idAcheteur;
            PrixDepart = prixDepart;
            PrixActuel = prixActuel;
            DateDepart = dateDepart;
            DateFin = dateFin;
            PasDePrix = pasDePrix;
        }

        public string Id { get; set; }
        public string IdEnchere { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public string IdCategorie { get; set; }
        public string Photo { get; set; }
        public string Piece { get; set; }
        public string IdVendeur { get; set; }
        public string IdAcheteur { get; set; }
        public decimal PrixDepart { get; set; }
        public decimal PrixActuel { get; set; }
        public DateTime DateDepart { get; set; }
        public DateTime DateFin { get; set; }
        public decimal PasDePrix { get; set; }
    }
}
