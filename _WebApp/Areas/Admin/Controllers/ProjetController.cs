using _WebApp.Areas.Admin.Infrastructure;
using _WebApp.Areas.Admin.Models.Formulaires;
using _WebApp.Areas.Admin.Models.ViewModels;
using Client.Models;
using Client.Services;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace _WebApp.Areas.Admin.Controllers {
    [AdminRequired]
    public class ProjetController : Controller
    {
        /***********************************************************************************************************
         ***********************************************Projet******************************************************
         ***********************************************************************************************************/

        public ActionResult Projet() {
            ProjetService ps = new ProjetService();

            return View(ps.GetAll());
        }

        /******************************Create********************************/
        public ActionResult CreateProjet() {
            return View();
        }

        [HttpPost]
        public ActionResult CreateProjet(CreateProjetForm form) {
            if (ModelState.IsValid) {
                ProjetService ps = new ProjetService();
                Projet p = new Projet(form.Nom, form.Description, form.DateDebut, form.DateFin, AdminSession.CurrentAdmin.NumeroAdmin);
                ps.Insert(p, form.IdEmp);

                return RedirectToAction("Projet", "Projet");
            }
            return View(form);
        }

        /******************************Details*******************************/
        public ActionResult DetailsProjet(int id) {
            ProjetService ps = new ProjetService();
            EquipeService eqs = new EquipeService();
            EmployeeService es = new EmployeeService();
            TacheEquipeService tes = new TacheEquipeService();

            AdminProjet ap = new AdminProjet();
            ap.p = ps.GetById(id);
            ap.e = es.GetManagerByProjet(id);
            ap.ListTask = tes.GetByProjet(id);
            IEnumerable<Equipe> listEq = eqs.GetByProjet(id);
            if (listEq != null && listEq.Any()) {
                foreach (Equipe eq in listEq) {
                    if (eq != null && eq.Id != null && eq.Id != 0) {
                        IEnumerable<Employee> listEmp = es.GetByEquipe((int)eq.Id).OrderBy(r => r.Nom);
                        if (listEmp != null && listEmp.Any())
                            ap.ListEqEmp.Add(eq, listEmp);
                    }
                }
            }

            return View(ap);
        }

        /*******************************Edit*********************************/
        public ActionResult EditProjet(int id) {
            ProjetService ps = new ProjetService();
            Projet p = ps.GetById(id);

            EditProjetForm form = new EditProjetForm();
            form.Id = (int)p.Id;
            form.Nom = p.Nom;
            form.Description = p.Description;
            form.DateDebut = p.Debut;
            form.DateFin = p.Fin;

            return View(form);
        }

        [HttpPost]
        public ActionResult EditProjet(EditProjetForm form) {
            if (ModelState.IsValid) {
                ProjetService ps = new ProjetService();
                Projet p = new Projet(form.Id, form.Nom, form.Description, form.DateDebut, form.DateFin, AdminSession.CurrentAdmin.NumeroAdmin);
                if (ps.Update(p))
                    return RedirectToAction("Projet", "Projet");
            }
            return View(form);
        }

        /******************************Affect********************************/
        public ActionResult AffecterEquipeProj(FormCollection listForm, int idProj) {
            if (listForm != null && listForm.Count >= 1) {
                listForm.Remove("idProj");
                List<int> listId = new List<int>();

                foreach (string item in listForm) {
                    listId.Add(int.Parse(item));
                }

                ProjetService ps = new ProjetService();
                ps.AffecterEquipe(listId, idProj);

                return RedirectToAction("DetailsProjet", "Projet", new { id = idProj });
            }
            EquipeService eqs = new EquipeService();
            return View(eqs.GetOtherByProjet(idProj));
        }
    }
}