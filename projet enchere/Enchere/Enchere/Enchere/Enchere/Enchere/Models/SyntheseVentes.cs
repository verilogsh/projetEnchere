using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enchere.Models
{
    public class SyntheseVentes
    {
        public string NomObjet { get; set; }
        public decimal Prix { get; set; }
        public DateTime Date { get; set; }
        public decimal Commision { get; set; }
        public decimal TotalVente { get; set; }
        public decimal TotalCommision { get; set; }

        public SyntheseVentes() { }

        public SyntheseVentes(string nomObjet, decimal prix, DateTime date, decimal commision, decimal totalVente, decimal totalCommision)
        {
            this.NomObjet = nomObjet;
            this.Prix = prix;
            this.Date = date;
            this.Commision = commision;
            this.TotalVente = totalVente;
            this.TotalCommision = totalCommision;
        }
    }
}