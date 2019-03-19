using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models {
    public class Notification {

        public int? Id { get; set; }
        public int Id_Employee { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string Contenu { get; set; }
        public Boolean Lu { get; set; }
        public int Id_Lien { get; set; }
    }
}
