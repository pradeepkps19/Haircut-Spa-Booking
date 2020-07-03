using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Xml;

namespace DatabaseFirstApp.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Admin()
        {
            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public ActionResult Logout()
        {
            Session["UserId"] = null;
            Session["UserName"] = null;
            Session.Abandon();
            return RedirectToAction("Login", "Home");
        }

    }
}