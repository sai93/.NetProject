using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ErrorLogModel;
using LoadersAndLogic;
using System.Web.Helpers;

namespace FinalProject.Controllers
{
    public class AdminController : Controller
    {
        

        // GET: Admin
        readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ActionResult Admin()
        {
            try
            {
                ProfHandler ph = new ProfHandler();
                
                if (Session["PROFILEID"] == null)
                {
                    throw new Exception();
                    //return RedirectToAction("Login", "Login");
                }
                int SessionId = Int32.Parse(Session["PROFILEID"].ToString());
                if (ph.checkUser(SessionId))
                {

                    return RedirectToAction("Logout", "Login");
                }
                return View();
            }
            catch(Exception ex)
            {
                logger.Debug("****************");
                logger.Error(ex.Message);
                logger.Debug(" Time Error Occured :"+DateTime.Now.ToString());
                logger.Debug("****************");
                return RedirectToAction("Logout", "Login");
            }
        }
        
        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                ProfHandler ph = new ProfHandler();

                if (Session["PROFILEID"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                int SessionId = Int32.Parse(Session["PROFILEID"].ToString());
                if (ph.checkUser(SessionId))
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Application app)
        {
            try
            {
                ProfHandler ph = new ProfHandler();
                AppHandler ah = new AppHandler();
                if (Session["PROFILEID"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                int SessionId = Int32.Parse(Session["PROFILEID"].ToString());
                if (ph.checkUser(SessionId))
                {
                    return RedirectToAction("Logout", "Login");
                }
                if (ModelState.IsValid)
                {
                    DateTime dt = DateTime.Parse(DateTime.Now.ToString());
                    app.CreaterId = (int)Session["PROFILEID"];
                    app.CreationTime = dt;
                    ah.addApp(app);

                    return RedirectToAction("Admin");
                }
                return View(app);
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
            try
            {
                ErrorLogHandler Errlg = new ErrorLogHandler();
            ProfHandler ph = new ProfHandler();
           
            if (Session["PROFILEID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            int SessionId = Int32.Parse(Session["PROFILEID"].ToString());
            if (ph.checkUser(SessionId))
            {
                return RedirectToAction("Logout", "Login");
            }
              Session["DAPPID"] = id;

                var appList = Errlg.getAErrorLog(id);

                return View(appList);
            }
            catch (Exception ex)
            {
                logger.Debug("****************");
                logger.Error(ex.Message);
                logger.Debug(" Time Error Occured :" + DateTime.Now.ToString());
                logger.Debug("****************");
                return RedirectToAction("ShowApplications");
            }
        }

        public ActionResult AssignUser(int? id)
        {
            try
            {
              ProfHandler ph = new ProfHandler();
            AppUsersHandler appUserh = new AppUsersHandler();            
            if (Session["PROFILEID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            int SessionId = Int32.Parse(Session["PROFILEID"].ToString());
            if (ph.checkUser(SessionId))
            {
                return RedirectToAction("Logout", "Login");
            }
           
                var UserProfiles = ph.getUsers();
                var AppUsers = appUserh.getAppUsers(id);
                Session["APPID"] = id;
                foreach (var appuser in AppUsers)
                {
                    Profile UserAssigned = ph.getProfile(appuser);
                    UserProfiles.Remove(UserAssigned);
                }
                return View(UserProfiles);
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

        public ActionResult Assign(int? id)
        {
            try
            {
                AppUsersHandler appUserh = new AppUsersHandler();
                ProfHandler ph = new ProfHandler();

                if (Session["PROFILEID"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                int SessionId = Int32.Parse(Session["PROFILEID"].ToString());
                if (ph.checkUser(SessionId))
                {
                    return RedirectToAction("Logout", "Login");
                }
                var profile = ph.getProfile(id);
                DateTime dt = DateTime.Parse(DateTime.Now.ToString());
                UsersAndApplication appusers = new UsersAndApplication();
                appusers.ApplicationId = (int)Session["APPID"];
                appusers.UserId = profile.ProfileId;
                appusers.TimeOfAssignment = dt;

                appUserh.addAppUser(appusers);
                return Redirect("~/Admin/AssignUser/" + (int)Session["APPID"]);
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
        
        
       
        
        public ActionResult Enable(int? id)
        {
            try { 
            ProfHandler ph = new ProfHandler();
            AppHandler ah = new AppHandler();
            if (Session["PROFILEID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            int SessionId = Int32.Parse(Session["PROFILEID"].ToString());
            if (ph.checkUser(SessionId))
            {
                return RedirectToAction("Logout", "Login");
            }
            
                ah.enableApp(id);
                
                
                return RedirectToAction("ShowApplications");
            }
            catch (Exception ex)
            {
                logger.Debug("****************");
                logger.Error(ex.Message);
                logger.Debug(" Time Error Occured :" + DateTime.Now.ToString());
                logger.Debug("****************");
                //Server.ClearError();
                return RedirectToAction("Logout", "Login");
            }
        }

        public ActionResult Users()
        {
            try
            {
                ProfHandler ph = new ProfHandler();
                if (Session["PROFILEID"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                int SessionId = Int32.Parse(Session["PROFILEID"].ToString());
                if (ph.checkUser(SessionId))
                {
                    return RedirectToAction("Logout", "Login");
                }
                // int SessionId = Int32.Parse(Session["PROFILEID"].ToString());
                return View(ph.getProfiles(SessionId));
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

        public ActionResult MakeAdmin(int? id)
        {
            try
            {
                ProfHandler ph = new ProfHandler();
                if (Session["PROFILEID"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                int SessionId = Int32.Parse(Session["PROFILEID"].ToString());
                if (ph.checkUser(SessionId))
                {
                    return RedirectToAction("Logout", "Login");
                }
                ph.makeAdmin(id);
                return RedirectToAction("Users");
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

        public ActionResult MakeActive(int? id)
        {
            try
            {
                ProfHandler ph = new ProfHandler();
                if (Session["PROFILEID"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                int SessionId = Int32.Parse(Session["PROFILEID"].ToString());
                if (ph.checkUser(SessionId))
                {
                    return RedirectToAction("Logout", "Login");
                }
                ph.makeActive(id);
                return RedirectToAction("Users");
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
                ProfHandler ph = new ProfHandler();
                AppHandler ah = new AppHandler();
                if (Session["PROFILEID"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                int SessionId = Int32.Parse(Session["PROFILEID"].ToString());
                if (ph.checkUser(SessionId))
                {

                    return RedirectToAction("Logout", "Login");
                }
                return View(ah.getApplications());
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

        public ActionResult Delete(int? id)
        {
            try
            {
                ProfHandler ph = new ProfHandler();
                AppHandler ah = new AppHandler();
                if (Session["PROFILEID"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                int SessionId = Int32.Parse(Session["PROFILEID"].ToString());
                if (ph.checkUser(SessionId))
                {
                    return RedirectToAction("Logout", "Login");
                }
                ah.delete(id);
                return RedirectToAction("ShowApplications");
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
                if (ph.checkUser(SessionId))
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
                if (ph.checkUser(SessionId))
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