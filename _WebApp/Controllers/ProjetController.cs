using _WebApp.Areas.Admin.Infrastructure;
using _WebApp.Infrastructure;
using _WebApp.Models.ViewModels;
using Client.Models;
using Client.Services;
using System.Linq;
using System.Web.Mvc;

namespace _WebApp.Controllers {
    [AuthRequired]
    public class ProjetController : Controller
    {
        /***********************************************************************************************************
         ************************************************Projet*****************************************************
         ***********************************************************************************************************/
        //public ActionResult Projet() {
        //    int IdEmp = (int)EmployeeSession.CurrentEmployee.Id;
        //    ProjetService ps = new ProjetService();
        //    return View(ps.GetByIdEmpl(IdEmp).First().Id);
        //}

        public ActionResult Projet(int id = 0) {
            int idMoi = (int)EmployeeSession.CurrentEmployee.Id;

            MemberProjet mp = new MemberProjet();
            ProjetService ps = new ProjetService();

            if (id == 0) {
                //Récupere le premier projet de l'utilisateur
                Projet p = ps.GetByIdEmpl(idMoi).FirstOrDefault();

                if (p != null && p.Id != null) {
                    mp.p = p;
                    id = (int)p.Id;
                }
                else {
                    //Aucun projet assigner
                    return RedirectToAction("Index", "Member");
                }
            }
            else {
                mp.p = ps.GetById(id);
            }

            EquipeService eqs = new EquipeService();
            mp.Equipes = eqs.GetByProjet(id);

            EmployeeService ems = new EmployeeService();
            //Verifie si l'utilisateur courrant a accès au projet 'id'
            Employee e = ems.GetByProjet(id).Where(r => r.Id == idMoi).SingleOrDefault();

            if (e != null || AdminSession.CurrentAdmin != null) {
                mp.chef = ems.GetManagerByProjet(id);

                TacheEmployeeService tes = new TacheEmployeeService();
                mp.TacheEmployees = tes.GetByEmployee(idMoi);

                TacheEquipeService teq = new TacheEquipeService();
                mp.TacheEquipes = teq.GetByProjet(id);

                MessageProjetService mps = new MessageProjetService();
                mp.MessageProjets = mps.GetSujetByProjet(id);

                DocumentService ds = new DocumentService();
                mp.Documents = ds.GetByProjet(id);

                return View(mp);
            }
            return RedirectToAction("Index", "Error");
        }
    }
}