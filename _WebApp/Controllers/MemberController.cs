using _WebApp.Infrastructure;
using _WebApp.Models.ViewModels;
using Client.Models;
using Client.Services;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;


namespace _WebApp.Controllers {
    [AuthRequired]
    public class MemberController : Controller
    {
        public ActionResult Index()
        {
            MemberIndex mi = new MemberIndex();
            if (EmployeeSession.CurrentEmployee.Id != null)
            {
                int idEmp = (int) EmployeeSession.CurrentEmployee.Id;

                ProjetService ps = new ProjetService();
                mi.ListP = ps.GetByIdEmpl(idEmp);

                List<Projet> listP = new List<Projet>(mi.ListP);
                int idP = (int)listP.First().Id;

                EmployeeService es = new EmployeeService();
                mi.ListE = es.GetCollegue(idEmp);

                Employee e = es.GetManagerByProjet(idP);
                mi.e = e;

                mi.ListEWDiscussion = es.GetWithDiscussion(idEmp);
            }

            MessageEmployeeService mes = new MessageEmployeeService();
            mi.ListME = mes.GetAll();

            return View(mi);
        }
        
        /***********************************************************************************************************
         ***********************************************Employee****************************************************
         ***********************************************************************************************************/
        public ActionResult Employee(int id) {
            if (id == EmployeeSession.CurrentEmployee.Id)
                return RedirectToAction("Index", "Profile");
            MemberDiscussion md = new MemberDiscussion();

            EmployeeService es = new EmployeeService();
            md.employee = es.GetById(id);

            if (md.employee.Adresse != 0) { 
                AdresseService ads = new AdresseService();
                md.adresse = ads.GetById(md.employee.Adresse);

                VilleService vs = new VilleService();
                md.ville = vs.GetById(md.adresse.Id_Ville);
            }

            if (EmployeeSession.CurrentEmployee.Id != null)
            {
                int idMoi = (int)EmployeeSession.CurrentEmployee.Id;
                MessageEmployeeService mes = new MessageEmployeeService();

                md.ListeMessageEmployees = mes.GetDiscution(idMoi, id);
            }

            return View(md);
        }
    }
}