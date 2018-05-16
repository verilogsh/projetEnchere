using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enchere.Models.ViewModel {
    public class ObjetViewModel
    {

        public ObjetViewModel(string id, string nom, string description, string categorie, decimal prixDepart, HttpPostedFileBase photo, HttpPostedFileBase piece)
        {
            Id = id;
            Nom = nom;
            Description = description;
            Categorie = categorie;
            PrixDepart = prixDepart;
            Photo = photo;
            Piece = piece;
        }

        public ObjetViewModel() { }

        public string Id { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public string Categorie { get; set; }
        public decimal PrixDepart { get; set; }
        public HttpPostedFileBase Photo { get; set; }
        public HttpPostedFileBase Piece { get; set; }
    }
   }