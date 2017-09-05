using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorLogModel;
using System.Net.Http;
//using System.Net.Http.Formatting;
using System.Threading;
using System.Web;
using System.Net.Http.Headers;

namespace DLLLibrary
{
    public class DLL
    {
        
        private static int ApplicationId;
        //private static int SERVICE_PORT = 57160;
        private static string SERVICE_URL = "http://localhost/LoggerService/";
        private static string ADD_ACTION = "Api/Rest/Post?log=";

        public DLL(int AppId)
        {
            ApplicationId = AppId;
        }
        public void debug( Exception E)
        {
            try
            {
                
                ErrorLog log = new ErrorLog();
                
                log.ApplicationId = ApplicationId;
                log.TypeId = 1;
                
                log.Name = E.GetType().Name;
                log.TimeStamp = DateTime.Now;
                log.Description = E.Message;
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(log);
                HttpContent content = new StringContent(json);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                string result = string.Empty;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(String.Format(SERVICE_URL/*, SERVICE_PORT*/));
                var task = client.PostAsync(ADD_ACTION, content).Result;

                

            }
            catch(Exception ex)
            {
                Console.Clear();
            }
        }
         public void warning(Exception E)
        {
            try
            {
               
                ErrorLog log = new ErrorLog();
                
                log.ApplicationId = ApplicationId;
                log.TypeId = 3;
                
                log.Name = E.GetType().Name;
                log.TimeStamp = DateTime.Now;
                log.Description = E.Message;
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(log);
                HttpContent content = new StringContent(json);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                string result = string.Empty;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(String.Format(SERVICE_URL/*, SERVICE_PORT*/));
                var task = client.PostAsync(ADD_ACTION, content).Result;
                
            }
            catch (Exception ex)
            {
                Console.Clear();
            }

        }
        public void error( Exception E)
        {
            try
            {
                
                ErrorLog log = new ErrorLog();
                
                log.ApplicationId = ApplicationId;
                log.TypeId = 2;
                
                log.Name = E.GetType().Name;
                log.TimeStamp = DateTime.Now;
                log.Description = E.Message;
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(log);
                HttpContent content = new StringContent(json);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                string result = string.Empty;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(String.Format(SERVICE_URL/*, SERVICE_PORT*/));
                var task = client.PostAsync(ADD_ACTION, content).Result;
                
            }
            catch(Exception ex)
            {
                Console.Clear();
            }
        }
    }
}
