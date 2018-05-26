using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enchere.Models
{
    public class Commission
    {
        public int Id { get; set; }
        public int Com { get; set; }

        public Commission() { }

        public Commission(int id, int com) {
            Id = id;
            Com = com;
        }
    }
}