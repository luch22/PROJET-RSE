using _WebApp.Infrastructure;
using _WebApp.Models.ViewModels;
using Client.Models;
using Client.Services;
using System.Linq;
using System.Web.Mvc;

namespace _WebApp.Controllers {
    [AuthRequired]
    public class TacheController : Controller
    {

        /***********************************************************************************************************
         ***********************************************Taches******************************************************
         ***********************************************************************************************************/

        public ActionResult TacheEquipe(int id) {
            int idMoi = (int)EmployeeSession.CurrentEmployee.Id;

            EmployeeService ems = new EmployeeService();
            //Verifie si l'utilisateur courrant a accès a la tache equipe 'id'
            Employee e = ems.GetByTacheEquipe(id).Where(r => r.Id == idMoi).SingleOrDefault();

            if (e != null) {
                MemberTacheEquipe mteq = new MemberTacheEquipe();

                TacheEquipeService teqs = new TacheEquipeService();
                mteq.te = teqs.GetById(id);

                MessageTacheService mteqq = new MessageTacheService();
                mteq.ListM = mteqq.GetSujetByTacheId(id);

                DocumentService ds = new DocumentService();
                mteq.ListD = ds.GetByTache(id);

                return View(mteq);
            }
            return RedirectToAction("Index", "Error");
        }
        public ActionResult TacheEmployee(int id) {
            int idMoi = (int)EmployeeSession.CurrentEmployee.Id;

            EmployeeService ems = new EmployeeService();
            //Verifie si l'utilisateur courrant a accès a la tache employee 'id'
            Employee e = ems.GetByTacheEmployee(id).Where(r => r.Id == idMoi).SingleOrDefault();

            if (e != null) {
                MemberTacheEmployee mte = new MemberTacheEmployee();

                TacheEmployeeService teqs = new TacheEmployeeService();
                mte.te = teqs.GetById(id);

                MessageTacheService mtes = new MessageTacheService();
                mte.ListM = mtes.GetSujetByTacheId(id);

                DocumentService ds = new DocumentService();
                mte.ListD = ds.GetByTache(id);

                return View(mte);
            }
            return RedirectToAction("Index", "Error");
        }
    }
}