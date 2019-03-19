using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public class StatutEmployee
    {
        public string NomStatut { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime? DateFin { get; set; }
        public int IdEmployee { get; set; }

        public StatutEmployee(string nom, DateTime datedebut, DateTime? datefin, int idemp) {
            NomStatut = nom;
            DateDebut = datedebut;
            DateFin = datefin;
            IdEmployee = idemp;
        }
    }
}
