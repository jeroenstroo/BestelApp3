using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BestelApp2.Models
{
    public class BestellingsProducten
    {
        public int ProductId { get; set; }
        public string ProductNaam { get; set; }
        public string CategorieNaam { get; set; }
        public bool Assigned { get; set; }
    }
}