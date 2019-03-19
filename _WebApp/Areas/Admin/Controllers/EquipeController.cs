using _WebApp.Areas.Admin.Infrastructure;
using _WebApp.Areas.Admin.Models.Formulaires;
using _WebApp.Areas.Admin.Models.ViewModels;
using Client.Models;
using Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace _WebApp.Areas.Admin.Controllers {
    [AdminRequired]
    public class EquipeController : Controller
    {
        /***********************************************************************************************************
         ***********************************************Equipe******************************************************
         ***********************************************************************************************************/

        public ActionResult Equipe() {
            EquipeService eqs = new EquipeService();
            EmployeeService es = new EmployeeService();

            AdminEquipe ae = new AdminEquipe();
            
            
            IEnumerable<Equipe> listEq = eqs.GetAll();
            if (listEq != null && listEq.Any()) {
                foreach (Equipe eq in listEq) {
                    if (eq != null && eq.Id != null && eq.Id != 0) {
                        IEnumerable<Employee> listEmp = es.GetByEquipe((int)eq.Id).OrderBy(r => r.Nom);
                        //if (listEmp != null && listEmp.Any())
                        ae.ListEqEmp.Add(eq, listEmp);
                        ae.ListChef.Add(eq, es.GetManagerByEquipe((int)eq.Id));
                    }
                }
            }

            return View(ae);
        }

        /******************************Create********************************/
        public ActionResult CreateEquipe() {
            EmployeeService es = new EmployeeService();

            CreateEquipeForm form = new CreateEquipeForm();
            form.ListEmp = es.GetAll();

            return View(form);
        }

        [HttpPost]
        public ActionResult CreateEquipe(CreateEquipeForm form, FormCollection formIds,int nomprojet) {
            if (ModelState.IsValid) {
                EquipeService eqs = new EquipeService();
                Equipe e = new Equipe(form.Nom, DateTime.Now, nomprojet);
                int idEq = (int)eqs.Insert(e).Id;

                List<int> listId = new List<int>();
                formIds.Remove("__RequestVerificationToken");
                formIds.Remove("Nom");
                formIds.Remove("Projet");
                foreach (string item in formIds) {
                    listId.Add(int.Parse(item));
                }

                eqs.AffecterEmployee(listId, idEq);

                return RedirectToAction("Equipe", "Equipe");
            }
            return View(form);
        }

        /******************************Details*******************************/
        public ActionResult DetailsEquipe(int id) {
            EquipeService eqs = new EquipeService();
            EmployeeService es = new EmployeeService();

            AdminEquipe ae = new AdminEquipe();
            ae.eq = eqs.GetById(id);
            ae.ListEqEmp.Add(ae.eq, es.GetByEquipe((int)ae.eq.Id));
            ae.ListChef.Add(ae.eq, es.GetManagerByEquipe((int)ae.eq.Id));

            return View(ae);
        }

        /******************************Affect********************************/
        public ActionResult AffecterEmployeeEquipe(FormCollection listForm, int idEq) {
            EquipeService eqs = new EquipeService();
            ViewBag.IdEq = idEq;
            if (listForm != null && listForm.Count >= 1) {
                listForm.Remove("idEq");
                List<int> listId = new List<int>();

                foreach (string item in listForm) {
                    listId.Add(int.Parse(item));
                }
                
                eqs.AffecterEmployee(listId, idEq);

                return RedirectToAction("DetailsEquipe", "Equipe", new { id = idEq });
            }
            EmployeeService es = new EmployeeService();
            return View(es.GetWithoutEquipe());
        }

        /******************************Delete********************************/
        public ActionResult SupprimerEmpEq(int eq, int emp) {
            EquipeService eqs = new EquipeService();
            eqs.RemoveEmployee(emp);

            return RedirectToAction("DetailsEquipe", new { id = eq});
        }
    }
}