using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _WebApp.Models.Formulaires
{
    public class InscriptionForms
    {
        [Required]
        [MaxLength(50)]
        [Display(Name = "Nom*:")]
        [DefaultValue("Entrez votre nom ici !")]
        public string Nom { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Prénom*:")]
        [DefaultValue("Entrez votre prenom ici !")]
        public string Prenom { get; set; }

        [Required]
        [MaxLength(249)]
        [Display(Name = "Email*:")]
        public string Email { get; set; }

        [Required]
        [MaxLength(19)]
        [MinLength(8)]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe*:")]
        public string Password { get; set; }

        [Required]
        [MaxLength(19)]
        [MinLength(8)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmation du mot de passe*:")]
        [Compare("Password", ErrorMessage = "La confirmation doit être identique au mot de passe!")]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.Date)]
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date de naissance*:")]
        public DateTime Birthday { get; set; }

        [Required]
        [MaxLength(20)]
        [Display(Name = "Registre national*:")]
        public string RegNat { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Adresse*:")]
        public string NomRue { get; set; }

        [Required]
        [MaxLength(7)]
        [Display(Name = "Numéro*:")]
        public string Numero { get; set; }

        [MaxLength(5)]
        [Display(Name = "Boite postal:")]
        public string Boite_Postal { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Ville*:")]
        public string Ville { get; set; }

        [Required]
        [MaxLength(7)]
        [Display(Name = "Zip*:")]
        public string Zip { get; set; }

        [Required]
        [Display(Name = "Pays*:")]
        public string Pays { get; set; }

        [DataType(DataType.Date)]
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date d'embauche*:")]
        public DateTime HireDate { get; set; }

        [Required]
        [MaxLength(25)]
        [Display(Name = "Numéro de téléphone*:")]
        public string Tel { get; set; }
    }
}