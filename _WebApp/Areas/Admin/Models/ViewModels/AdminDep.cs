using Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _WebApp.Areas.Admin.Models.ViewModels {
    public class AdminDep {

        public Departement dep { get; set; }
        public Employee e { get; set; }
        public Document doc { get; set; }

        public IEnumerable<Employee> ListEmpDep { get; set; } = null;
        public IEnumerable<Document> ListD { get; set; }
    }
}