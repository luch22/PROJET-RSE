using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models {
    public class Rue {

        public string Nom_Rue { get; set; }
        public int Id_Ville { get; set; }

        public Rue(string nomR, int idVille) {
            Nom_Rue = nomR;
            Id_Ville = idVille;
        }
    }
}
