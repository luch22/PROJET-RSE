using _WebApp.Infrastructure;
using Client.Models;
using Client.Services;
using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace _WebApp.Controllers {
    [AuthRequired]
    public class FileManagerController : Controller
    {
        public ActionResult Upload(HttpPostedFileBase file, string des, string type, int id)
        {
            if (file != null && file.ContentLength > 0 && !string.IsNullOrWhiteSpace(des)) {

                try {
                    byte[] fileContents;
                    using (StreamReader sourceStream = new StreamReader(file.InputStream)) {
                        fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
                        sourceStream.Close();
                    }

                    if (fileContents.Length <= 52428800) {
                        DocumentService ds = new DocumentService();
                        
                        Document d = new Document(new FileInfo(file.FileName).Name, des, DateTime.Now, fileContents, fileContents.Length, file.ContentType, (int)EmployeeSession.CurrentEmployee.Id, null);

                        ds.Insert(d, type, id);

                        ViewBag.Message = "Fichier uploader";
                    }
                    else {
                        ViewBag.Message = "Le fichier ne doit pas excéder 25 mb!";
                    }
                }
                catch (Exception ex) {
                    ViewBag.Message = "ERROR:" + ex.Message;
                }
            }
            else if (file != null && string.IsNullOrWhiteSpace(des)) {
                ViewBag.Message = "Veuillez écrire une description";
            }
            else {
                ViewBag.Message = "Vous n'avez pas choisi de fichier";
            }
            ViewBag.Type = type;
            ViewBag.Id = id;

            return View();
        }

        public ActionResult Download(int id) {
            DocumentService ds = new DocumentService();
            Document d = ds.GetById(id);
            return File(d.Contenu, System.Net.Mime.MediaTypeNames.Application.Octet, d.Nom);
        }
    }
}