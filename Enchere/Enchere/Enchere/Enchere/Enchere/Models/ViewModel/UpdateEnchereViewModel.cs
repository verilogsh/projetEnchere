using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Enchere.Models.ViewModel {
    public class UpdateEnchereViewModel {
        public string Id { get; set; }
        public string IdObjet { get; set; }
        public string IdVendeur { get; set; }
        public string IdAcheteur { get; set; }
        public decimal Prix { get; set; }
        [Required]
        public decimal PrixAchat { get; set; }
        [Required]
        public decimal PasDePrix { get; set; }
        [Required]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime DateDepart { get; set; }
        [Required]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime DateFin { get; set; }
        [Required]
        public int Etat { get; set; }
    }
}