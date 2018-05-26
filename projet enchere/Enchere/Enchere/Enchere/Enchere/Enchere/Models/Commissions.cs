using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enchere.Models
{
    public class Commissions
    {
        public int Id { get; set; }
        public int Taux { get; set; }
        public Decimal Prix { get; set; }
        public String IdEnchere { get; set; }
        public DateTime Date { get; set; }

        public Commissions() { }

        public Commissions(int id, int taux, Decimal prix, String idEnchere, DateTime date)
        {
            this.Id = id;
            this.Taux = taux;
            this.Prix = prix;
            this.IdEnchere = idEnchere;
            this.Date = date;
        }
    }
}