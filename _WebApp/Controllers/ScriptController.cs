using _WebApp.Infrastructure;
using Client.Models;
using Client.Services;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace _WebApp.Controllers {
    [AuthRequired]
    public class ScriptController : Controller
    {
        public string AdresseRue(string input, string zip) {
            if (input != null && zip != null && !string.IsNullOrWhiteSpace(input) && !string.IsNullOrWhiteSpace(zip) && input.Length >= 5 ) {
                RueService rs = new RueService();
                IEnumerable<Rue> rues = rs.Search(input, int.Parse(""+zip.ToCharArray()[0]+zip.ToCharArray()[1]));

                if (rues != null && rues.Any()) {
                    string nomRue = "";
                    foreach (Rue r in rues) {
                        nomRue += (r.Nom_Rue + ";");
                    }
                    return nomRue.Remove(nomRue.Length - 1);
                }
            }
            return null;
        }

        public string ZipVille(string zip)
        {
            if (zip != null && !string.IsNullOrWhiteSpace(zip)) {
                VilleService vs = new VilleService();
                IEnumerable<Ville> villes = vs.GetByZip(zip);
                
                if (villes != null && villes.Any()) {
                    string nomVilles = "";
                    foreach (Ville v in villes)
                        nomVilles += (v.Nom+";");
                    return nomVilles.Remove(nomVilles.Length -1);
                }
            }
            return null;
        }

        public string Pays(string input) {
            if (input != null && !string.IsNullOrWhiteSpace(input) && input.Length >= 2) {
                PaysService vs = new PaysService();
                IEnumerable<Pays> pays = vs.Search(input);

                if (pays != null && pays.Any()) {
                    string nomPays = "";
                    foreach (Pays p in pays)
                        nomPays += (p.Nom_FR + ";");
                    return nomPays.Remove(nomPays.Length - 1);
                }
            }
            return null;
        }

        public string Employees(string input) {
            if (input != null && !string.IsNullOrWhiteSpace(input)) {
                EmployeeService es = new EmployeeService();
                IEnumerable<Employee> listE = es.GetByNomPrenom(input);

                if (listE != null && listE.Any()) {
                    string nomEmp = "";
                    foreach (Employee e in listE)
                        nomEmp += (e.Nom+" "+e.Prenom+" "+e.Id+";");
                    return nomEmp.Remove(nomEmp.Length - 1);
                }
            }
            return null;
        }

        public ActionResult Notifications() {
            int idMoi = (int)EmployeeSession.CurrentEmployee.Id;
            if (idMoi != 0) {
                NotificationService ns = new NotificationService();

                JsonResult jr = Json(from notif in ns.GetByEmployee(idMoi)
                                select new {
                                    Contenu = notif.Contenu,
                                    Date = notif.Date,
                                    Lien = new Switch<string, string>(notif.Type).Case("EmployeeEquipe").Then("/Equipe/Equipe/" + notif.Id_Lien).Case("EmployeeDep").Then("#").Case("EquipeProjet").Then("/Projet/Projet/" + notif.Id_Lien).Case("MessageEmployee").Then("/Member/Employee/" + notif.Id_Lien).Case("MessageProjet").Then("/Message/MessageProjet/" + notif.Id_Lien).Case("MessageEquipe").Then("/Message/MessageEquipe/" + notif.Id_Lien).Case("MessageTache").Then("/Message/MessageTacheEquipe/" + notif.Id_Lien).Default("#"),
                                    Lu = new Switch<bool, string>(notif.Lu).Case(true).Then("#FFFFFF").Default("#00bc8c")
                                });
                return jr;
            }
            return null;
        }

        public void MarkRead() {
            int idMoi = (int)EmployeeSession.CurrentEmployee.Id;
            if (idMoi != 0) {
                NotificationService ns = new NotificationService();
                ns.MarkAsRead(idMoi);
            }
        }
    }
}