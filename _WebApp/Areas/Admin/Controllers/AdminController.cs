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
    public class AdminController : Controller
    {
        /***********************************************************************************************************
         *********************************************Departement***************************************************
         ***********************************************************************************************************/

        public ActionResult Index()
        {
            DepartementService ds = new DepartementService();
            
            return View(ds.GetAll());
        }

        /******************************Create********************************/
        public ActionResult CreateDep() {
            return View();
        }

        [HttpPost]
        public ActionResult CreateDep(CreateDepForm form) {
            if (ModelState.IsValid) {
                DepartementService ds = new DepartementService();
                Departement d = new Departement(form.Nom, form.Description, AdminSession.CurrentAdmin.NumeroAdmin);
                d = ds.Insert(d);

                return RedirectToAction("Index", "Admin");
            }
            return View(form);
        }

        /******************************Details*******************************/
        public ActionResult DetailsDep(int id) {
            DepartementService ds = new DepartementService();
            EmployeeService es = new EmployeeService();
            DocumentService dos = new DocumentService();

            AdminDep ad = new AdminDep();
            ad.dep = ds.GetById(id);
            ad.ListEmpDep = es.GetByDep(id).Where(r => r.Id != 1).OrderBy(r => r.Nom);
            ad.ListD = dos.GetByDep(id);

            return View(ad);
        }

        /*******************************Edit*********************************/
        public ActionResult EditDep(int id) {
            DepartementService ds = new DepartementService();
            Departement d = ds.GetById(id);

            EditDepForm form = new EditDepForm();
            form.Id = (int)d.Id;
            form.Nom = d.Nom;
            form.Description = d.Description;

            return View(form);
        }

        [HttpPost]
        public ActionResult EditDep(EditDepForm form) {
            if (ModelState.IsValid) {
                DepartementService ds = new DepartementService();
                Departement d = new Departement(form.Id, form.Nom, form.Description, AdminSession.CurrentAdmin.NumeroAdmin);
                if (ds.Update(d))
                    return RedirectToAction("Index", "Admin");
            }
            return View(form);
        }

        /******************************Delete********************************/
        public ActionResult DeleteDep(int id)
        {
            if (id != 0)
            {
                DepartementService ds = new DepartementService();
                return View(ds.GetById(id));
            }
            return View();
        }

        
        public ActionResult DeleteDep2(int id) {
            DepartementService ds = new DepartementService();
            if (ds.Delete(id))
                return RedirectToAction("Index", "Admin");
            return RedirectToAction("DeleteDep", "Admin", id);
        }

        /******************************Affect********************************/
        public ActionResult AffecterEmployeeDep(FormCollection listForm, int idDep) {
            ViewBag.IdDep = idDep;
            if (listForm != null && listForm.Count >= 1) {
                listForm.Remove("idDep");
                List<int> listId = new List<int>();

                foreach (string item in listForm) {
                    listId.Add(int.Parse(item));
                }

                DepartementService ds = new DepartementService();
                ds.AffecteEmployee(listId, idDep);

                return RedirectToAction("DetailsDep", "Admin", new { id = idDep });
            }
            EmployeeService es = new EmployeeService();
            return View(es.GetWithoutDep(idDep).OrderBy(r => r.Nom));
        }

        /******************************Remove********************************/
        public ActionResult SupprimerEmpDep(int dep, int emp) {
            DepartementService dps = new DepartementService();
            dps.RemoveEmployee(dep, emp);

            return RedirectToAction("DetailsDep", new { id = dep });
        }
    }
}