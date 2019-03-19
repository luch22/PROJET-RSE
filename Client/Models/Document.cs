using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models {
    public class Document
    {
        public int? Id { get; private set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public byte[] Contenu { get; set; }
        public int Taille { get; set; }
        public string Format { get; set; }
        public int Id_Emp_Creee { get; set; }
        public int? Id_Emp_Maj { get; set; }

        public Document() {

        }

        public Document(string nom, string des, DateTime date, byte[] contenu, int taille, string format, int idEmpCree, int? idEmpMaj) {
            Nom = nom;
            Description = des;
            Date = date;
            Contenu = contenu;
            Taille = taille;
            Format = format;
            Id_Emp_Creee = idEmpCree;
            Id_Emp_Maj = idEmpMaj;
        }

        public Document(int? id, string nom, string des, DateTime date, byte[] contenu, int taille, string format, int idEmpCree, int? idEmpMaj) : this(nom, des, date, contenu, taille, format, idEmpCree, idEmpMaj) {
            Id = id;
        }
    }
}
