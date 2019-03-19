using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _WebApp.Models.Formulaires
{
    public class TacheForms
    {
        [Required]
        [MaxLength(50)]
        public string Nom { get; set; }

        [Required]
        [MaxLength(250)]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        [Required]
        [DataType("datetime-local")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyThh:mm}", ApplyFormatInEditMode = true)]
        public DateTime Debut { get; set; }

        [DataType("datetime-local")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyThh:mm}", ApplyFormatInEditMode = true)]
        public DateTime Fin { get; set; }

        [DataType("datetime-local")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyThh:mm}", ApplyFormatInEditMode = true)]
        public DateTime Final { get; set; }


        public int? TachePrecedente { get; set; }

        [Required]
        public int Projet { get; set; }

        public Boolean TacheEquipe { get; set; }


    }
}