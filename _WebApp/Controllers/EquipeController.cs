using _WebApp.Areas.Admin.Infrastructure;
using _WebApp.Infrastructure;
using _WebApp.Models.ViewModels;
using Client.Models;
using Client.Services;
using System.Linq;
using System.Web.Mvc;

namespace _WebApp.Controllers {
    [AuthRequired]
    public class EquipeController : Controller
    {
        /***********************************************************************************************************
         ***********************************************Equipe******************************************************
         ***********************************************************************************************************/
        //public ActionResult Equipe()
        //{
        //    EmployeeService es = new EmployeeService();
        //    return View(es.GetByEquipe((int) EmployeeSession.CurrentEmployee.Id).First().Id);
        //}

        public ActionResult Equipe(int id = 0) {
            int idMoi = (int)EmployeeSession.CurrentEmployee.Id;

            MemberEquipe me = new MemberEquipe();

            EquipeService eqs = new EquipeService();
            Equipe eq;
            if (id != 0) {
                eq = eqs.GetById(id);
            }
            else {
                eq = eqs.GetByEmployee(idMoi);
            }

            if (eq != null) {
                int? idEq = eq.Id;

                if (idEq != null && idEq != 0) {
                    EmployeeService ems = new EmployeeService();
                    //Verifie si l'utilisateur courrant a accès à l'équipe 'idEq'
                    Employee e = ems.GetByEquipe((int)idEq).Where(r => r.Id == idMoi).SingleOrDefault();

                    if (e != null || AdminSession.CurrentAdmin != null) {
                        me.eq = eq;
                        me.ListE = ems.GetByEquipe((int)idEq);

                        MessageEquipeService mes = new MessageEquipeService();
                        me.ListMEq = mes.GetSujetByEquipe((int)idEq);

                        DocumentService ds = new DocumentService();
                        me.ListD = ds.GetByEquipe((int)idEq);

                        me.emp = ems.GetManagerByEquipe((int)idEq);

                        return View(me);
                    }
                }
            }
            return RedirectToAction("Index", "Error");
        }
    }
}