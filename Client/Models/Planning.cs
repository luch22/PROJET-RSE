using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models {
    public class Planning {
        
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Type { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFinal { get; set; }
        public bool FullDay { get; set; }

        public Planning(int id, string nom, string type, DateTime dateDebut, DateTime dateFinal, bool fullDay) {
            Id = id;
            Nom = nom;
            Type = type;
            DateDebut = dateDebut;
            DateFinal = dateFinal;
            FullDay = fullDay;
        }
    }
}
