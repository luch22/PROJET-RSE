using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _WebApp.Areas.Admin.Models.Formulaires {
    public class CreateProjetForm {

        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        public string Nom { get; set; }

        [Required]
        [MaxLength(250)]
        [MinLength(2)]
        public string Description { get; set; }

        [Required]
        [DataType("datetime-local")]
        [DisplayName("Date de début*:")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyThh:mm}", ApplyFormatInEditMode = true)]
        public DateTime DateDebut { get; set; }

        [DataType("datetime-local")]
        [DisplayName("Date de fin:")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyThh:mm}", ApplyFormatInEditMode = true)]
        public DateTime? DateFin { get; set; }

        [Required(ErrorMessage = "Veuillez selectionner un manager de projet!")]
        public int IdEmp { get; set; }
    }
}