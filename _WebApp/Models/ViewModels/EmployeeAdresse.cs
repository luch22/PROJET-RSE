using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Client.Models;

namespace _WebApp.Models.ViewModels {
    public class EmployeeAdresse
    {
        public Employee Employee { get; set; }

        public Adresse Adresse { get; set; }

        public Ville Ville { get; set; }


    }
}