using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Client.Models;

namespace _WebApp.Models.ViewModels
{
    public class AdresseVille
    {
        public Event Event { get; set; }

        public Adresse Adresse { get; set; }

        public Ville Ville { get; set; }

    }
}