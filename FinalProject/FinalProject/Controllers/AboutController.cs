using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoadersAndLogic;
using ErrorLogModel;


namespace FinalProject.Controllers
{
    public class AboutController : Controller
    {
        // GET: Index
        public ActionResult About()
        {
            try
            {

                return View();
            }
            catch
            {
                return RedirectToAction("Logout", "Login");
            }
        }
        
    }
}