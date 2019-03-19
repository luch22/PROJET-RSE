using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _WebApp.Models.Formulaires
{
    public class EditEventForms
    {
        
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        [DisplayName("Nom*:")]
        public string Nom { get; set; }


        [Required]
        [DisplayName("Description*:")]
        [DataType(DataType.Text)]
        public string Description { get; set; }
    
        
        [Required]
        [DataType("datetime-local")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        [DisplayName("Date et heure de début*:")]
        public DateTime DateDebut { get; set; }
        
        [Required]
        [DataType("datetime-local")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        [DisplayName("Date et heure de fin:")]
        public DateTime DateFin { get; set; }
        
        [DisplayName("Journée entière:")]
        public bool FullDay { get; set; }

        [Required]
        [DisplayName("Adresse*:")]
        public string NomRue { get; set; }

        [Required]
        [MaxLength(7)]
        [Display(Name = "Numéro*:")]
        public string Numero { get; set; }

        [MaxLength(5)]
        [DisplayName("Boite postal:")]
        public string Boite_Postal { get; set; }

        [Required]
        [DisplayName("Ville*:")]
        public string Ville { get; set; }

        [Required]
        [DisplayName("Code postal*:")]
        public string Zip { get; set; }
    }
}