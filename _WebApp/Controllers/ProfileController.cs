using _WebApp.Infrastructure;
using _WebApp.Models.Formulaires;
using Client.Models;
using Client.Services;
using System.Web.Mvc;

namespace _WebApp.Controllers {
    [AuthRequired]
    public class ProfileController : Controller
    {
        public ActionResult Index()
        {
            Profile p = new Profile();
            AdresseService ads = new AdresseService();
            VilleService vs = new VilleService();
            p.E = EmployeeSession.CurrentEmployee;
            p.Ad = ads.GetById(p.E.Adresse);
            p.Vi = vs.GetById(p.Ad.Id_Ville);
            return View(p);
        }

        [HttpPost]
        public ActionResult Index(Profile form) {
            if (form.New == form.Confirm) {
                EmployeeService es = new EmployeeService();
                Employee e = es.GetByEmailPassword(EmployeeSession.CurrentEmployee.Email, form.Password);
                TempData["changePass"] = "Mot de passe incorrect !";
                if (e != null)
                {
                    if (e.Id != null) es.ChangePassword((int) e.Id, form.New);
                    TempData["changePass"] = "Mot de passe changer avec succès";
                }
            }
            return RedirectToAction("Index", "Profile");
        }
    }
}