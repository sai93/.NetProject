using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ErrorLogModel;
using LoadersAndLogic;
using System.Web.Helpers;


namespace FinalProject.Controllers
{
    public class UserController : Controller
    {
        
        ProfHandler ph = new ProfHandler();

        // GET: User
        readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ActionResult User()
        {
            try
            {
                //  Appl
                AppUsersHandler appUserh = new AppUsersHandler();
                if (Session["PROFILEID"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                if ((bool)Session["ISACTIVE"] == false)
                {
                   
                        return RedirectToAction("Logout", "Login");
                    
                }
                int SessionId = Int32.Parse(Session["PROFILEID"].ToString());
                if (!ph.checkUser(SessionId))
                {

                    return RedirectToAction("Logout", "Login");
                }
              
                return View();
            }
            catch (Exception ex)
            {
                logger.Debug("****************");
                logger.Error(ex.Message);
                logger.Debug(" Time Error Occured :" + DateTime.Now.ToString());
                logger.Debug("****************");

                return RedirectToAction("Logout", "Login");
            }
        }

        public ActionResult Details(int? id)
        {
            try { 
            ErrorLogHandler ErrHan = new ErrorLogHandler();
            if(Session["PROFILEID"] == null)
            {
                return RedirectToAction("Login","Login");
            }

                if ((bool)Session["ISACTIVE"] == false)
                {

                    return RedirectToAction("Logout", "Login");

                }
                int SessionId = Int32.Parse(Session["PROFILEID"].ToString());
                if (!ph.checkUser(SessionId))
                {

                    return RedirectToAction("Logout", "Login");
                }
                
                Session["DAPPID"] = id;
                var appList = ErrHan.getErrorLog(id, SessionId);

                return View(appList);
            }
            catch (Exception ex)
            {
                logger.Debug("****************");
                logger.Error(ex.Message);
                logger.Debug(" Time Error Occured :" + DateTime.Now.ToString());
                logger.Debug("****************");
                return RedirectToAction("Logout", "Login");
            }
        }

        public ActionResult ShowApplications()
        {
            try
            {
                AppUsersHandler appUserh = new AppUsersHandler();
                if (Session["PROFILEID"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                if ((bool)Session["ISACTIVE"] == false)
                {

                    return RedirectToAction("Logout", "Login");

                }
                int SessionId = Int32.Parse(Session["PROFILEID"].ToString());
                if (!ph.checkUser(SessionId))
                {

                    return RedirectToAction("Logout", "Login");
                }
                
                if (Session["PROFILEID"] != null)
                {

                    int id = Int32.Parse(Session["PROFILEID"].ToString());
                    var userProfiles = appUserh.getAppsOfUser(id); /*db.UsersAndApplications.Where(ua => ua.Profile.ProfileId == id && ua.Application.IsActive == true).ToList();*/
                    return View(userProfiles);

                }
                return RedirectToAction("Logout", "Login");


            }
            catch (Exception ex)
            {
                logger.Debug("****************");
                logger.Error(ex.Message);
                logger.Debug(" Time Error Occured :" + DateTime.Now.ToString());
                logger.Debug("****************");
                return RedirectToAction("Logout", "Login");
            }
        }

        public ActionResult MyChart()
        {
            try
            {
                ProfHandler ph = new ProfHandler();
                ErrorLogHandler errh = new ErrorLogHandler();
                if (Session["PROFILEID"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                int SessionId = Int32.Parse(Session["PROFILEID"].ToString());
                if (!ph.checkUser(SessionId))
                {

                    return RedirectToAction("Logout", "Login");
                }

                int id = Int32.Parse(Session["DAPPID"].ToString());
                var db = new sjinkaDataBaseEntities();
                var testChart = new Chart(width: 300, height: 300)
                    .AddTitle("Test ErrorLog")
                    .AddSeries(
                        name: "Errors",
                        xValue: new[] { "Debug", "Error", "Warning" },
                        yValues: new[] { errh.getCount(id, 1).ToString(), errh.getCount(id, 2).ToString(), errh.getCount(id, 3).ToString() })
                    .GetBytes("png");
                Session["DAPPID"] = id;
                return File(testChart, "image/png");
            }
            catch (Exception ex)
            {
                logger.Debug("****************");
                logger.Error(ex.Message);
                logger.Debug(" Time Error Occured :" + DateTime.Now.ToString());
                logger.Debug("****************");
                return RedirectToAction("Logout", "Login");
            }
        }

        public ActionResult DispPie()
        {
            try
            {
                ProfHandler ph = new ProfHandler();
                ErrorLogHandler errh = new ErrorLogHandler();
                if (Session["PROFILEID"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                int SessionId = Int32.Parse(Session["PROFILEID"].ToString());
                if (!ph.checkUser(SessionId))
                {

                    return RedirectToAction("Logout", "Login");
                }

                int id = Int32.Parse(Session["DAPPID"].ToString());
                Chart ch = new Chart(300, 300).AddTitle("Error Types");
                string[] xval = new[] { "Debug", "Error", "Warning" };
                int[] yval = new[] { errh.getCount(id, 1), errh.getCount(id, 2), errh.getCount(id, 3) };
                ch.AddSeries("Default", chartType: "Pie",
                    xValue: xval, yValues: yval).Write("png");
                ch.Save("~/Content/chart", "png");
                return File("~/Content/chart", "png");
            }
            catch (Exception ex)
            {
                logger.Debug("****************");
                logger.Error(ex.Message);
                logger.Debug(" Time Error Occured :" + DateTime.Now.ToString());
                logger.Debug("****************");
                return RedirectToAction("Logout", "Login");
            }
        }
    }
}