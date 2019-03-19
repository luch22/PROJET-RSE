using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models {
    public class Notification {

        public int? Id { get; set; }
        public int Id_Employee { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string Contenu { get; set; }
        public Boolean Lu { get; set; }
        public int Id_Lien { get; set; }

        public Notification(int idEmp, DateTime date, string type, string contenu, bool lu, int idLien) {
            Id_Employee = idEmp;
            Date = date;
            Type = type;
            Contenu = contenu;
            Lu = lu;
            Id_Lien = idLien;
        }

        public Notification(int? id, int idEmp, DateTime date, string type, string contenu, bool lu, int idLien) : this(idEmp, date, type, contenu, lu, idLien) {
            Id = id;
        }
    }
}
