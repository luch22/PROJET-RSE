using Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _WebApp.Areas.Admin.Models.ViewModels {
    public class AdminEquipe {

        public Equipe eq { get; set; }

        public Dictionary<Equipe, IEnumerable<Employee>> ListEqEmp { get; set; } = new Dictionary<Equipe, IEnumerable<Employee>>();

        public Dictionary<Equipe, Employee> ListChef { get; set; } = new Dictionary<Equipe, Employee>(); 
    }
}