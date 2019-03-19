using Client.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _WebApp.Areas.Admin.Models.Formulaires {
    public class CreateEquipeForm {

        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        public string Nom { get; set; }

        [Required]
        public int Projet { get; set; }

        public IEnumerable<Employee> ListEmp { get; set; }

        public Employee e { get; set; }
    }
}