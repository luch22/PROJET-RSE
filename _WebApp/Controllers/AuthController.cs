using _WebApp.Areas.Admin.Infrastructure;
using _WebApp.Infrastructure;
using _WebApp.Models.Formulaires;
using Client.Models;
using Client.Services;
using System.Web.Mvc;

namespace _WebApp.Controllers {
    public class AuthController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        [AnonymousRequired]
        public ActionResult Register() {
            return View();
        }

        [AnonymousRequired]
        [HttpPost]
        public ActionResult Register(InscriptionForms form) {
            if (ModelState.IsValid) {
                //Récupere le pays depuis la base de données
                PaysService ps = new PaysService();
                Pays p = ps.GetByName(form.Pays);

                if (p != null && p.Id != null) {
                    //Récupere la ville depuis la base de données
                    VilleService vs = new VilleService();
                    Ville v = vs.GetByNomZipPays(form.Ville, form.Zip, (int)p.Id);

                    if (v != null && v.Id != null) {
                        AdresseService ads = new AdresseService();
                        Adresse a = new Adresse(form.NomRue, form.Numero, form.Boite_Postal, (int)v.Id);
                        int? id = ads.Insert(a).Id;
                        if (id != null && id != 0)
                        {
                            EmployeeService ur = new EmployeeService();
                            Employee e = new Employee(form.Nom, form.Prenom, form.Email, form.Password, form.Birthday, form.RegNat, (int)id, form.HireDate, form.Tel, null, true, false);
                            var i = ur.Insert(e).Id;
                            if (i != null && i != 0) {
                                //TODO Sucess else msg erreur
                                ModelState.Clear();
                                return RedirectToAction("Index", "Home");
                            }
                        }
                        ViewBag.Message = "Une erreur c'est produit avec la base de données, veuillez réessayer plus tard!";
                    }
                    else {
                        ViewBag.Message = "Nous n'avons pas pu trouver la ville renseignée dans notre base de données";
                    }
                }
            }
            return View(form);
        }

        [AnonymousRequired]
        public ActionResult Login() {
            return View();
        }

        [AnonymousRequired]
        [HttpPost]
        public ActionResult Login(LoginForm form) {
            if (ModelState.IsValid) {
                EmployeeService ur = new EmployeeService();
                Employee e = ur.GetByEmailPassword(form.Login, form.Password);
                if (e != null && e.Id != null)
                {
                    EmployeeSession.CurrentEmployee = e;

                    //Vérifie si l'utilisateur est un admin
                    AdministrateurService ads = new AdministrateurService();
                    Administrateur a = ads.GetByIdEmployee((int)e.Id);
                    if (a != null && a.Id != null) {
                        AdminSession.CurrentAdmin = a;
                    }

                    return RedirectToAction("Index", "Member", new { area = "" });
                }
                else {
                    ModelState.Clear();
                    return View();
                }
            }
            return View(form);
        }

        [AuthRequired]
        public ActionResult Logout() {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}