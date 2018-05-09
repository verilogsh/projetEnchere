using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enchere.Models {
    public class Objet {
        public string Id { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public DateTime DateInscri { get; set; }
        public string IdCategorie { get; set; }
        public string Photo { get; set; }
        public string Piece { get; set; }
        public string IdMembre { get; set; }
        public bool Nouveau { get; set; }
        public bool EnVent { get; set; }
        public decimal PrixDepart { get; set; }

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