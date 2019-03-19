using Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _WebApp.Areas.Admin.Infrastructure {
    public class AdminSession {
        
        public static Administrateur CurrentAdmin {
            get {
                return (Administrateur)HttpContext.Current.Session["Admin"];
            }
            set {
                HttpContext.Current.Session["Admin"] = value;
            }
        }
    }
}