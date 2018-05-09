using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enchere.Models {
    public class Membrebk {
        public string Id { get; set; }
        public string Civilite { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Langue { get; set; }
        public string Telephone { get; set; }
        public string Adresse { get; set; }
        public string Courriel { get; set; }
        public DateTime DateInscri { get; set; }
        public string MotDePasse { get; set; }
        public int Cote { get; set; }

        public Membrebk(string id, string civilite, string nom, string prenom, string langue, string telephone, string adresse, string courriel, DateTime dateInscri, string motDePasse, int cote) {
            Id = id;
            Civilite = civilite;
            Nom = nom;
            Prenom = prenom;
            Langue = langue;
            Telephone = telephone;
            Adresse = adresse;
            Courriel = courriel;
            DateInscri = dateInscri;
            MotDePasse = motDePasse;
            Cote = cote;
        }
    }
}