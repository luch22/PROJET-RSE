using _WebApp.Infrastructure;
using Client.Services;
using System;
using System.Linq;
using System.Web.Mvc;

namespace _WebApp.Controllers {
    [AuthRequired]
    public class PlanningController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetEvent(DateTime start, DateTime end) {
            int id = (int)EmployeeSession.CurrentEmployee.Id;
            PlanningService ps = new PlanningService();

            JsonResult jr = Json(from planning in ps.GetByEmployee(id, start, end)
                                    select new {
                                        Nom = planning.Nom,
                                        DateDebut = ((planning.DateDebut.Year == start.Year) ? planning.DateDebut : (planning.DateDebut.Year == end.Year) ? planning.DateDebut : planning.DateDebut.AddYears(end.Year - planning.DateDebut.Year)),
                                        DateFin = ((planning.DateFinal.Year == start.Year) ? planning.DateFinal : (planning.DateFinal.Year == end.Year) ? planning.DateFinal : planning.DateFinal.AddYears(1)),
                                        FullDay = planning.FullDay ? true : false,
                                        Color = new Switch<string, string>(planning.Type).Case("Tache_Employee").Then("#248254").Case("Tache_Equipe").Then("#0f78e9").Case("Event").Then("#f39c12").Case("Anniversaire").Then("#f11040").Case("DebutProjet").Then("#0661ef").Case("FinProjet").Then("#723215").Default("#808080"),
                                        Url = new Switch<string, string>(planning.Type).Case("Tache_Employee").Then("/Tache/TacheEmployee/"+planning.Id).Case("Tache_Equipe").Then("/Tache/TacheEquipe/"+planning.Id).Case("Event").Then("/Event/DetailsEvent/"+planning.Id).Case("Anniversaire").Then("/Member/Employee/"+planning.Id).Case("DebutProjet").Then("/Projet/Projet/"+planning.Id).Case("FinProjet").Then("/Projet/Projet/"+planning.Id).Default("NaN")
                                    });

            return jr;
        }
    }
}