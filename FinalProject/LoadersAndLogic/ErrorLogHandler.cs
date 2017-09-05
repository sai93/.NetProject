using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorLogModel;


namespace LoadersAndLogic
{
    public class ErrorLogHandler
    {
        sjinkaDataBaseEntities db = new sjinkaDataBaseEntities();
        public List<ErrorLog> getErrorLog(int? AppId, int SessionId)
        {
            if(db.UsersAndApplications.Any(ua => ua.UserId == SessionId && ua.ApplicationId == AppId))
            {
                return db.ErrorLogs.Where(emp => emp.ApplicationId == AppId).ToList();
            }
            return new List<ErrorLog>();
        }

        public List<ErrorLog> getAErrorLog(int? AppId)
        {
           
                return db.ErrorLogs.Where(emp => emp.ApplicationId == AppId).ToList();
           
        }

        public void addErrorLog(ErrorLog log)
        {
            db.ErrorLogs.Add(log);
            db.SaveChanges();
        }

        public int getCount(int? AppId,int type)
        {
            return db.ErrorLogs.Count(er => er.ApplicationId == AppId && er.TypeId == type);
        }

      public List<LogType> getErrTypes()
        {
            return db.LogTypes.ToList();
        } 

    }
}
