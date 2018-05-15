using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enchere.Models
{
    public class Historique
    {
        //public int Id { get; set; }
        public string IdMembre{ get; set; }
        public string IdEnchere{ get; set; }
        public float Prix{ get; set; }
        public DateTime Date{ get; set; }

        //public Historique() { }
        public Historique( string idMembre, string idEnchere, float prix, DateTime date)
        {
           // this.Id = id;
            this.IdMembre = idMembre;
            this.IdEnchere = idEnchere;
            this.Prix = prix;
            this.Date = date;

        }
    }
}