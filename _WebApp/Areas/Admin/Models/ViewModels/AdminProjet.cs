using Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _WebApp.Areas.Admin.Models.ViewModels {
    public class AdminProjet {

        public Projet p { get; set; }
        public TacheEquipe te { get; set; }
        public Equipe eq { get; set; }
        public Employee e { get; set; }

        public IEnumerable<TacheEquipe> ListTask { get; set; }
        public Dictionary<Equipe, IEnumerable<Employee>> ListEqEmp { get; set; } = new Dictionary<Equipe, IEnumerable<Employee>>();
    }
}