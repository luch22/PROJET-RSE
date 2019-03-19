using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _WebApp.Areas.Admin.Models.Formulaires {
    public class CreateTaskForm {

        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        [DisplayName("Nom*:")]
        public string Nom { get; set; }

        [Required]
        [MaxLength(250)]
        [MinLength(3)]
        [DisplayName("Description*:")]
        public string Description { get; set; }

        [Required]
        [DataType("date")]
        [DisplayName("Date de début*:")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}", ApplyFormatInEditMode = true)]
        public DateTime Debut { get; set; }

        [DataType("date")]
        [DisplayName("Deadline:")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}", ApplyFormatInEditMode = true)]
        public DateTime? Final { get; set; }

        public int Precedente { get; set; }

        [Required]
        [DisplayName("Type de tache*:")]
        public string Type { get; set; }

        public int? Employee { get; set; }
        public int? Equipe { get; set; }
    }
}