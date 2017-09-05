using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace FinalProject
{
    public class MvcApplication : System.Web.HttpApplication
    {
        readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/Web.config")));
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
           
        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

            // Get the exception object.
            Exception exc = Server.GetLastError();

            // Handle HTTP errors
            if (exc.GetType() == typeof(HttpException))
            {
               
                logger.Debug("****************");
                logger.Error(exc.Message);
                logger.Debug(" Time Error Occured :" + DateTime.Now.ToString());
                logger.Debug("****************");
                Server.ClearError();
                Response.Redirect("~/Error/Error");
            }
            else
            {
               
                logger.Debug("****************");
                logger.Error(exc.Message);
                logger.Debug(" Time Error Occured :" + DateTime.Now.ToString());
                logger.Debug("****************");
                Server.ClearError();
                Response.Redirect("~/Error/Error");
            }
            logger.Debug("****************");
            logger.Error(exc.Message);
            logger.Debug(" Time Error Occured :" + DateTime.Now.ToString());
            logger.Debug("****************");
            Server.ClearError();
            Response.Redirect("~/Error/Error");


        }


    }
}
