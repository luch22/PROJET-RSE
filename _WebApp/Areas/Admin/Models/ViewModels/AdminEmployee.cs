using Client.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _WebApp.Areas.Admin.Models.ViewModels {
    public class AdminEmployee {
        public Employee e { get; set; }
        public Departement d { get; set; }
        public IPagedList<Tuple<Employee, Departement>> listE { get; set; }
        public IEnumerable<Departement> listD { get; set; }
    }
}