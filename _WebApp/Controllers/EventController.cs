using _WebApp.Infrastructure;
using _WebApp.Models.Formulaires;
using _WebApp.Models.ViewModels;
using Client.Models;
using Client.Services;
using System.Collections.Generic;
using System.Web.Mvc;


namespace _WebApp.Controllers {
    [AuthRequired]
    public class EventController : Controller
    {
        // GET: Event
        public ActionResult Index()
        {
            EventService eventService = new EventService();
           
            return View(eventService.GetAll());
        }

        public ActionResult DetailsEvent(int id)
        {
            EventService evs = new EventService();

            EmployeeService es = new EmployeeService();

            AdresseService ads = new AdresseService();

            VilleService vs = new VilleService();

            EventEmployee ee = new EventEmployee {Eve = evs.GetById(id)};

            ee.Employee = es.GetById(ee.Eve.Id_Employee);

            ee.EventAdresse.Adresse = ads.GetById(ee.Eve.Lieu);
            ee.EventAdresse.Ville = vs.GetById(ee.EventAdresse.Adresse.Id_Ville);
            IEnumerable<Employee> listE = es.GetByEvent(id);
            List<EmployeeAdresse> listA = new List<EmployeeAdresse>();

            foreach (var item in listE)
            {
                EmployeeAdresse adresse = new EmployeeAdresse {Employee = item};
                adresse.Adresse = ads.GetById( item.Adresse);
                if (adresse.Adresse != null)
                {
                    adresse.Ville = vs.GetById(adresse.Adresse.Id_Ville);
                }
                
                listA.Add(adresse);
            }
            ee.ListEmp = listA;

           // ListE.Where(r =>)
           // TODO A FAIRE EMPLOYEE BY ADDRESS IN EVENT

            //EventEmployee ee = new EventEmployee
            //{
            //    ListEmp = employeeService.GetByEvent(id),
            //    Eve = eventService.GetById(id)
            //};
            
            return View(ee);
            
        }

        public ActionResult CreateEvent(){

            return View();
        }

        [HttpPost]
        public ActionResult CreateEvent(EventForms form)
        {
            if (ModelState.IsValid)
            {
                VilleService vs = new VilleService();
                Ville v = vs.GetByNomZipPays(form.Ville, form.Zip, 18);
                if (v != null && v.Id != null)
                {
                    AdresseService ads = new AdresseService();
                    Adresse a = new Adresse(form.NomRue, form.Numero, form.Boite_Postal, (int) v.Id);
                    //TODO INSERT Adresse qui exsiste déjà
                    a = ads.Insert(a);
                    if (a.Id != null && EmployeeSession.CurrentEmployee.Id != null)
                    {
                        EventService evs = new EventService();
                        Event e = new Event(form.Nom, form.Description, (int) a.Id, form.DateDebut, form.DateFin, form.FullDay, (int)EmployeeSession.CurrentEmployee.Id);
                        e = evs.Insert(e);
                        if (e.Id != null)
                            return RedirectToAction("Index", "Event");
                    }
                }
            }

            return View(form);
        }
        
        public ActionResult RechercheVille(int? lieu)
        {
            if (lieu != null) {
                AdresseService ad = new AdresseService();
                VilleService vi = new VilleService();

                Adresse a = ad.GetById((int)lieu);
                Ville v = vi.GetById(a.Id_Ville);
                return Redirect("http://maps.google.fr/maps?f=q&hl=fr&q=" + a.Nom_Rue +","+ a.Numero +" "+ v.Nom);
            }
            return RedirectToAction("Index", "Event");
        }

        public ActionResult ParticipeEvent(int idEp, int idEv)
        {
            EventService evs = new EventService();
            evs.Participe(idEp, idEv);
            return RedirectToAction("DetailsEvent", "Event",new {id = idEv});
        }

        public ActionResult EditEvent(int id)
        {
            EventService es = new EventService();
            AdresseVille ee = new AdresseVille();
            AdresseService ads = new AdresseService();
            VilleService vs = new VilleService();

            ee.Event = es.GetById(id);
            ee.Adresse = ads.GetById(ee.Event.Lieu);
            ee.Ville = vs.GetById(ee.Adresse.Id_Ville);

            if (ee.Event.Id != null)
            {
                EditEventForms ef = new EditEventForms()
                { //TODO FAIRE LE NUMERO POUR LES ADRESSE DANS LA DB STP 
                    Id = (int)ee.Event.Id,
                    Boite_Postal = ee.Adresse.Boite_Postal,
                    Zip = ee.Ville.Zip,
                    Ville = ee.Ville.Nom,
                    NomRue = ee.Adresse.Nom_Rue,
                    Numero = ee.Adresse.Numero,
                    Nom = ee.Event.Nom,
                    DateDebut = ee.Event.DateDebut,
                    DateFin = ee.Event.DateFin,
                    Description = ee.Event.Description,
                    FullDay = ee.Event.FullDay
                };
                return View(ef);
            }

            return View();
        }
        [HttpPost]
        public ActionResult EditEvent(EditEventForms form)
        {
             EventService es = new EventService();
            AdresseService ads = new AdresseService();
            VilleService vs = new VilleService();
            if (ModelState.IsValid)
            {
                AdresseVille ee = new AdresseVille {Ville = vs.GetByNomZipPays(form.Ville, form.Zip, 18)};
                if (ee.Ville.Id != null)
                {
                    Adresse a = new Adresse(form.NomRue, form.Numero, form.Boite_Postal,(int)ee.Ville.Id);
                    a.Id = ads.GetByRueVille(a);
                    

                    if (a.Id != null)
                    {
                        Event eve = new Event(form.Id, form.Nom, form.Description, (int) a.Id, form.DateDebut,
                            form.DateFin,
                            form.FullDay, 0);
                        es.Update(eve);
                        return RedirectToAction("DetailsEvent",new {id = form.Id});
                    }
                }
                
                
            }

            return View(form);

        }

        public void DeleteEvent(int id)
        {
            EventService es = new EventService();
            es.Delete(id);
        }

    }
 
}