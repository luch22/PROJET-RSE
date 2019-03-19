using _WebApp.Areas.Admin.Infrastructure;
using _WebApp.Areas.Admin.Models.Formulaires;
using Client.Models;
using Client.Services;
using System.Web.Mvc;

namespace _WebApp.Areas.Admin.Controllers {
    [AdminRequired]
    public class TaskController : Controller
    {

        public ActionResult CreateTask(int idProj, int? idTask)
        {
            Session["idP"] = idProj;
            Session["idT"] = idTask;
            return View();
        }

        [HttpPost]
        public ActionResult CreateTask(CreateTaskForm form) {
            if (ModelState.IsValid) {
                TacheEquipe teq = null;
                TacheEmployee temp = null;
                if (form.Type.Equals("Equipe") && form.Equipe != null) {
                    TacheEquipeService tes = new TacheEquipeService();
                    teq = new TacheEquipe(form.Nom, form.Description, form.Debut, null, form.Final, (int?)Session["idT"], (int)Session["idP"]);
                    teq = tes.Insert(teq, (int)form.Equipe);
                }
                else if (form.Employee != null){
                    TacheEmployeeService tes = new TacheEmployeeService();
                    temp = new TacheEmployee(form.Nom, form.Description, form.Debut, null, form.Final, (int?)Session["idT"], (int)Session["idP"]);
                    temp = tes.Insert(temp, (int)form.Employee);
                }
                if ((teq != null && teq.Id != null) || (temp != null && temp.Id != null))
                    return RedirectToAction("Projet", "Projet", new { id = (int)Session["idP"], area = "Admin" });
            }
            return View(form);
        }
    }
}