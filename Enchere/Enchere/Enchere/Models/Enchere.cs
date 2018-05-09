using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enchere.Models {
    public class Enchere {
        public string Id { get; set; }
        public string IdMembre { get; set; }
        public decimal prixAchat { get; set; }
        public DateTime DateDepart { get; set; }
        public int DureeVent { get; set; }

        public Enchere(string id, string idMembre, decimal prixAchat, DateTime dateDepart, int dureeVent) {
            Id = id;
            IdMembre = idMembre;
            this.prixAchat = prixAchat;
            DateDepart = dateDepart;
            DureeVent = dureeVent;
        }
    }
}