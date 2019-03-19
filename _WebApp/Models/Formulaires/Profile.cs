using Client.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _WebApp.Models.Formulaires
{
    public class Profile
    {

        public Employee E { get; set; }

        public Adresse Ad { get; set; }

        public Ville Vi {get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MaxLength(19)]
        [MinLength(8)]
        [DisplayName("Mot de passe:")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MaxLength(19)]
        [MinLength(8)]
        [DisplayName("Nouveau mot de passe:")]
        public string New { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MaxLength(19)]
        [MinLength(8)]
        [DisplayName("Confirmation du mot de passe:")]
        [Compare("New")]
        public string Confirm { get; set; }
    }

}