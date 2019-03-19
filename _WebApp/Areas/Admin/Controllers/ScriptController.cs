using _WebApp.Areas.Admin.Infrastructure;
using Client.Models;
using Client.Services;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace _WebApp.Areas.Admin.Controllers {
    [AdminRequired]
    public class ScriptController : Controller
    {
        public string GetNomProjet()
        {
            ProjetService ps = new ProjetService();
            IEnumerable<Projet> projets = ps.GetAll();
            string nomProjet = "";

            if (projets != null && projets.Any())
                {
                     foreach (Projet pro in projets)
                {
                    nomProjet += pro.Nom + "|" + pro.Id + ";";
                }
                return nomProjet.Remove(nomProjet.Length -1);
                }

            return nomProjet;
        }
           
    }
}