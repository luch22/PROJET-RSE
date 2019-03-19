﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models {
     public class Employee {
        public int? Id { get; private set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [DisplayFormat(DataFormatString = "{0:d MMMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Birthday { get; set; }
        public string RegNat { get; set; }
        public int Adresse { get; set; }

        [DisplayFormat(DataFormatString = "{0:d MMMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime HireDate { get; set; }
        public string Tel { get; set; }
        public int? Coordonnee { get; set; }
        public bool Actif { get; set; }
        public bool Valide { get; set; }

        public Employee() {

        }

        public Employee(string nom, string prenom, string email, string pass, DateTime birthday, string regnat, int adresse, DateTime hiredate, string tel, int? coord, bool actif, bool valide) {
            Nom = nom;
            Prenom = prenom;
            Email = email;
            Password = pass;
            Birthday = birthday;
            RegNat = regnat;
            Adresse = adresse;
            HireDate = hiredate;
            Tel = tel;
            Coordonnee = coord;
            Actif = actif;
            Valide = valide;
        }

        public Employee(int? id, string nom, string prenom, string email, string pass, DateTime birthday, string regnat, int adresse, DateTime hiredate, string tel, int? coord, bool actif, bool valide) : this(nom, prenom, email, pass, birthday, regnat, adresse, hiredate, tel, coord, actif, valide) {
            Id = id;
        }

         public string Fullname()
         {

             return Nom +" "+ Prenom;
         }
    }
   
}
