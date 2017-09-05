using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ErrorLogModel;
using LoadersAndLogic;



namespace FinalProject.Controllers
{
    public class LoginController : Controller
    {
        
        ProfHandler prof = new ProfHandler();

        // GET: Login
        readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        [HttpGet]
        public ActionResult Login()
        {
            
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                logger.Debug("****************");
                logger.Error(ex.Message);
                logger.Debug(" Time Error Occured :" + DateTime.Now.ToString());
                logger.Debug("****************");
                return RedirectToAction("Logout");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Profile profile)
        {
            try
                {
                Profile LoggedProfile = Authentication.AuthenticateUser(profile.Email, profile.Password);

                if (LoggedProfile != null)
                {

                    if (LoggedProfile.IsActive != true)
                    {
                        return View();
                    }


                    prof.UpdateLastLogin(LoggedProfile.ProfileId);
                    // LoggedProfile.LastLoginDate = DateTime.Now;
                    Session["PROFILEID"] = LoggedProfile.ProfileId;
                    Session["FIRSTNAME"] = LoggedProfile.FirstName;
                    Session["LASTNAME"] = LoggedProfile.LastName;
                    Session["Email"] = LoggedProfile.Email;
                    Session["ISADMIN"] = LoggedProfile.IsAdmin;
                    Session["ISACTIVE"] = LoggedProfile.IsActive;
                    Session["LASTLOGINDATE"] = LoggedProfile.LastLoginDate;



                    if (LoggedProfile.IsActive == true && LoggedProfile.IsAdmin)
                    {
                        return RedirectToAction("Admin", "Admin");
                    }
                    else if(LoggedProfile.IsActive == true)
                    {
                        return RedirectToAction("User", "User");
                    }
                    return RedirectToAction("Logout");

                }
                    return View();
                
           
                   
                }   
                    
            catch (Exception ex)
            {
                logger.Debug("****************");
                logger.Error(ex.Message);
                logger.Debug(" Time Error Occured :" + DateTime.Now.ToString());
                logger.Debug("****************");
                return View();
                }

           
           
        }

        [HttpGet]
        public ActionResult Register()
        {
            
                return View();
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Profile profile)
        {
            try
            {
                profile.IsAdmin = false;
                profile.IsActive = true;
                profile.LastLoginDate = DateTime.Now;
                if (ModelState.IsValid)
                {
                    
                        Authentication.RegisterUser(profile);


                        return RedirectToAction("Login");
                    

                }
                return View();
            }
            catch (Exception ex)
            {
                logger.Debug("****************");
                logger.Error(ex.Message);
                logger.Debug(" Time Error Occured :" + DateTime.Now.ToString());
                logger.Debug("****************");
                return View();
            }
            
        }
        public ActionResult afReg()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                logger.Debug("****************");
                logger.Error(ex.Message);
                logger.Debug(" Time Error Occured :" + DateTime.Now.ToString());
                logger.Debug("****************");
                return RedirectToAction("Logout");
            }
        }

        public ActionResult Logout()
        {
            
                Session.Clear();
                Session.Abandon();



            return RedirectToAction("Login");
        }

        
    }
}