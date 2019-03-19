using _WebApp.Areas.Admin.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace _WebApp.Infrastructure {
    public class AnonymousRequiredAttribute : AuthorizeAttribute {
        protected override bool AuthorizeCore(HttpContextBase httpContext) {
            if (EmployeeSession.CurrentEmployee == null || AdminSession.CurrentAdmin == null)
                return true;
            else
                return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext) {
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Member", action = "Index" }));
        }
    }
}