using Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _WebApp.Areas.Admin.Models.ViewModels {
    public class DetailEmployee {

        public Employee e { get; set; }
        public Adresse a { get; set; }
        public Ville v { get; set; }
        public StatutEmployee se { get; set; }
        public IEnumerable<StatutEmployee> listStatut { get; set; }
    }
}