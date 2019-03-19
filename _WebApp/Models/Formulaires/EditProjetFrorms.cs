using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _WebApp.Models.Formulaires
{
    public class EditProjetFrorms
    {
        [Required]
        [MaxLength(250)]
        [DataType(DataType.Text)]
        public string Description { get; set; }
        
        [Required]
        [DataType("datetime-local")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyThh:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date de Fin")]
        public DateTime? Fin { get; set; }

        public int? Admin { get; set; }
    }
}