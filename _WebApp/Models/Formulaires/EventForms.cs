using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _WebApp.Models.Formulaires
{
    public class EventForms
    {
        [Required]
        [MaxLength(50)]
        [DisplayName("Nom*:")]
        public string Nom { get; set; }


        [Required]
        [MaxLength(250)]
        [DisplayName("Description*:")]
        [DataType(DataType.Text)]
        public string Description { get; set; }
    
        
        [Required]
        [DataType("datetime-local")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyyTHH:mm}", ApplyFormatInEditMode = true)]
        [DisplayName("Date et heure de début*:")]
        public DateTime DateDebut { get; set; }
        
        [Required]
        [DataType("datetime-local")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyyTHH:mm}", ApplyFormatInEditMode = true)]
        [DisplayName("Date et heure de fin:")]
        public DateTime DateFin { get; set; }

        [Range(typeof(bool), "false", "true")]
        [DisplayName("Journée entière:")]
        public bool FullDay { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("Rue & numéro*:")]
        public string NomRue { get; set; }

        [Required]
        [MaxLength(5)]
        [Display(Name = "Numéro*:")]
        public string Numero { get; set; }

        [MaxLength(5)]
        [DisplayName("Boite postal:")]
        public string Boite_Postal { get; set; }

        [Required]
        [DisplayName("Ville*:")]
        public string Ville { get; set; }

        [Required]
        [MaxLength(7)]
        [DisplayName("Code postal*:")]
        public string Zip { get; set; }
    }
}