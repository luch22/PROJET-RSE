using _WebApp.Areas.Admin.Infrastructure;
using _WebApp.Areas.Admin.Models.ViewModels;
using Client.Models;
using Client.Services;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace _WebApp.Areas.Admin.Controllers {
    [AdminRequired]
    public class EmployeeController : Controller
    {
        public ActionResult Employee(string search, string filter, int? page, int? size) {
            EmployeeService es = new EmployeeService();
            DepartementService ds = new DepartementService();

            List<Tuple<Employee, Departement>> listE = new List<Tuple<Employee, Departement>>();
            foreach (Employee emp in es.GetAll().OrderBy(e => e.Nom)) {
                if (emp.Id != null)
                {
                    Departement d = ds.GetByEmployee((int)emp.Id);
                    listE.Add(Tuple.Create(emp, d));
                }
            }

            if (search != null) {
                page = 1;
            }
            else {
                search = filter;
            }
            ViewBag.CurrentFilter = search;

            if (!string.IsNullOrEmpty(search)) {
                listE = listE.Where(s => s.Item1.Nom.ToLower().Contains(search.ToLower()) || s.Item1.Prenom.ToLower().Contains(search.ToLower())).ToList();
            }

            int pageSize = 10;
            if (size != null && size >= 10)
                pageSize = (int)size;
            int pageNumber = (page ?? 1);
            AdminEmployee ae = new AdminEmployee {listE = listE.ToPagedList(pageNumber, pageSize), listD = ds.GetAll()};


            return View(ae);
        }

        public ActionResult DetailsEmployee(int id)
        {
            EmployeeService es = new EmployeeService();
            StatutEmployeeService ses = new StatutEmployeeService();

            DetailEmployee de = new DetailEmployee{ e = es.GetById(id), listStatut = ses.GetByEmployee(id).OrderByDescending(s => s.DateDebut) };

            if (de.e.Adresse != 0) {
                AdresseService ads = new AdresseService();
                de.a = ads.GetById(de.e.Adresse);

                VilleService vs = new VilleService();
                de.v = vs.GetById(de.a.Id_Ville);
            }

            return View(de);
        }

        public void ChangerDep (int idemp, int iddep) {
            DepartementService ds = new DepartementService();
            var list = new List<int> {idemp};
            ds.AffecteEmployee(list, iddep);
        }

        public void ActiveEmployee (int id) {
            EmployeeService es = new EmployeeService();
            es.ActiveEmployee(id);
        }

        public void DeleteEmployee (int id) {
            EmployeeService es = new EmployeeService();
            es.Delete(id);
        }
    }
}