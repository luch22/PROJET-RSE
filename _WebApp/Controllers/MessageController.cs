using _WebApp.Areas.Admin.Infrastructure;
using _WebApp.Infrastructure;
using _WebApp.Models.Formulaires;
using Client.Models;
using Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace _WebApp.Controllers {
    [AuthRequired]
    public class MessageController : Controller
    {

        /***********************************************************************************************************
         ***********************************************Message*****************************************************
         ***********************************************************************************************************/

        public ActionResult MessageEquipe(int id) {
            int idMoi = (int)EmployeeSession.CurrentEmployee.Id;
            MessageEquipeService mes = new MessageEquipeService();
            IEnumerable<MessageEquipe> me = mes.GetMessageBySujet(id);
            int idEq = 0;
            foreach(MessageEquipe me2 in me) {
                idEq = me2.Id_Equipe;
                break;
            }
            EmployeeService es = new EmployeeService();
            Employee e = es.GetByEquipe(idEq).Where(r => r.Id == idMoi).SingleOrDefault();

            if (e != null || AdminSession.CurrentAdmin != null)
                return View(me);
            return RedirectToAction("Index", "Error");
        }

        public ActionResult MessageProjet(int id) {
            int idMoi = (int)EmployeeSession.CurrentEmployee.Id;
            MessageProjetService mps = new MessageProjetService();
            IEnumerable<MessageProjet> mp = mps.GetMessageBySujet(id);
            int idP = 0;
            foreach(MessageProjet mp2 in mp) {
                idP = mp2.Id_Projet;
                break;
            }
            EmployeeService es = new EmployeeService();
            Employee e = es.GetByProjet(idP).Where(r => r.Id == idMoi).SingleOrDefault();

            if (e != null || AdminSession.CurrentAdmin != null)
                return View(mp);
            return RedirectToAction("Index", "Error");
        }

        public ActionResult MessageTacheEquipe(int id) {
            int idMoi = (int)EmployeeSession.CurrentEmployee.Id;
            MessageTacheService mts = new MessageTacheService();
            IEnumerable<MessageTache> mt = mts.GetMessageBySujet(id);
            int idT = 0;
            foreach(MessageTache mt2 in mt) {
                if(mt2.Id_Tache_Equipe != null) {
                    idT = (int)mt2.Id_Tache_Equipe;
                }
                break;
            }
            if(idT != 0) {
                EmployeeService es = new EmployeeService();
                Employee e = es.GetByTacheEquipe(idT).Where(r => r.Id == idMoi).SingleOrDefault();
                if (e != null || AdminSession.CurrentAdmin != null)
                    return View(mt);
            }
            return RedirectToAction("Index", "Error");
        }

        /***********************************************************************************************************
         *******************************************RéponseMessage**************************************************
         ***********************************************************************************************************/

        [HttpPost]
        public ActionResult RepondreEmployee(int idDest, int? idMsg, string msg) {
            int idMoi = (int)EmployeeSession.CurrentEmployee.Id;

            if (!string.IsNullOrWhiteSpace(msg) && idDest != 0) {
                MessageEmployee me = new MessageEmployee { Titre = idMoi + "" + idDest, Contenu = msg, Date = DateTime.Now, Id_Employee = idMoi, Id_Destinataire = idDest, MessagePrecedent = idMsg };

                MessageEmployeeService mes = new MessageEmployeeService();
                mes.Insert(me);
            }

            return RedirectToAction("Employee", "Member", new { id = idDest });
        }

        [HttpPost]
        public ActionResult RepondreEquipe(int idEq, int? idMsg, string msg, string titr) {
            int idMoi = (int)EmployeeSession.CurrentEmployee.Id;

            if (!string.IsNullOrWhiteSpace(msg) && idEq != 0) {
                MessageEquipe me = new MessageEquipe { Titre = titr, Contenu = msg, Date = DateTime.Now, Id_Employee = idMoi, Id_Equipe = idEq, MessagePrecedent = idMsg };

                MessageEquipeService mes = new MessageEquipeService();
                mes.Insert(me);
            }

            return RedirectToAction("Equipe", "Equipe", new { id = idMsg });
        }

        [HttpPost]
        public ActionResult RepondreProjet(int idPr, int? idMsg, string msg,string titr) {
            int idMoi = (int)EmployeeSession.CurrentEmployee.Id;

            if (!string.IsNullOrWhiteSpace(msg) && idPr != 0) {
                MessageProjet mp = new MessageProjet { Titre = titr, Contenu = msg, Date = DateTime.Now, Id_Employee = idMoi, Id_Projet = idPr, MessagePrecedent = idMsg };

                MessageProjetService mps = new MessageProjetService();
                mps.Insert(mp);
            }

            return RedirectToAction("MessageProjet", "Message", new { id = idMsg });
        }

        [HttpPost]
        public ActionResult RepondreTacheEquipe(int idTa, int? idMsg, string msg, string titr) {
            int idMoi = (int)EmployeeSession.CurrentEmployee.Id;

            if (!string.IsNullOrWhiteSpace(msg) && idTa != 0) {
                MessageTache mt = new MessageTache { Titre =  titr, Contenu = msg, Date = DateTime.Now, Id_Employee = idMoi, Id_Tache_Employee = null, Id_Tache_Equipe = idTa, MessagePrecedent = idMsg };

                MessageTacheService mps = new MessageTacheService();
                mps.Insert(mt);
            }

            return RedirectToAction("TacheEquipe", "Tache", new { id = idMsg });
        }

        /***********************************************************************************************************
         *********************************************NouveauSujet**************************************************
         ***********************************************************************************************************/

        public ActionResult CreateSujetEquipe(int id) {
            TempData["idEq"] = id;
            return View();
        }

        [HttpPost]
        public ActionResult CreateSujetEquipe(SujetForm form) {
            int idMoi = (int)EmployeeSession.CurrentEmployee.Id;

            if (ModelState.IsValid) {
                MessageEquipeService mes = new MessageEquipeService();
                MessageEquipe me = new MessageEquipe(form.Titre, DateTime.Now, form.Message, null, idMoi, (int)TempData["idEq"], null);

                int idmsg = (int)mes.Insert(me).Id;
                return RedirectToAction("MessageEquipe", "Message", new { id = idmsg });
            }
            return View(form);
        }

        public ActionResult CreateSujetProjet(int id) {
            TempData["idPr"] = id;
            return View();
        }

        [HttpPost]
        public ActionResult CreateSujetProjet(SujetForm form) {
            int idMoi = (int)EmployeeSession.CurrentEmployee.Id;

            if (ModelState.IsValid) {
                MessageProjetService mps = new MessageProjetService();
                MessageProjet mp = new MessageProjet(form.Titre, DateTime.Now, form.Message, null, idMoi, (int)TempData["idPr"], null);

                int idmsg = (int)mps.Insert(mp).Id;
                return RedirectToAction("MessageProjet", "Message", new { id = idmsg });
            }
            return View(form);
        }

        public ActionResult CreateSujetTache(int id) {
            TempData["idTa"] = id;
            return View();
        }

        [HttpPost]
        public ActionResult CreateSujetTache(SujetForm form) {
            if (EmployeeSession.CurrentEmployee.Id != null)
            {
                int idMoi = (int)EmployeeSession.CurrentEmployee.Id;

                if (ModelState.IsValid) {
                    MessageTacheService mts = new MessageTacheService();
                    MessageTache mt = new MessageTache(form.Titre, DateTime.Now, form.Message, null, idMoi, (int)TempData["idPr"], null, null);

                    var id = mts.Insert(mt).Id;
                    if (id != null)
                    {
                        int idmsg = (int)id;
                        return RedirectToAction("MessageTacheEquipe", "Message", new { id = idmsg });
                    }
                }
            }

            return View(form);
        }
    }
}