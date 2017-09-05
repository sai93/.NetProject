using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ErrorLogModel;
using LoadersAndLogic;
using System.Collections.Concurrent;
using System.Threading;


namespace LoggerService.Controllers
{
    public class RestController : ApiController
    {
        BlockingCollection<ErrorLog> BlkQueue =  new BlockingCollection<ErrorLog>();
        
        [HttpPost]
        public void Post(ErrorLog log)
        {
            try
            {
                BlkQueue.Add(log);
                if (BlkQueue.Count() > 0)
                {

                    ErrorLog loger = BlkQueue.Take();

                    Thread newThread = new Thread(x =>
                   {

                       ErrorLogHandler errhan = new ErrorLogHandler();
                        errhan.addErrorLog(loger);

                   });
                    newThread.Start();

                }

            }
            catch (Exception ex)
            {

            }
        }
    }
}
