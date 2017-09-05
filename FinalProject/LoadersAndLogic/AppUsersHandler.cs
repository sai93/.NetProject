using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorLogModel;


namespace LoadersAndLogic
{
    public class AppUsersHandler
    {
        sjinkaDataBaseEntities db = new sjinkaDataBaseEntities();
        public List<UsersAndApplication> getAppUsers(int? id)
        {
            return db.UsersAndApplications.Where(ua => ua.ApplicationId == id).ToList();
        }

        public void addAppUser(UsersAndApplication appuser)
        {
            db.UsersAndApplications.Add(appuser);
            db.SaveChanges();
        }

        public List<UsersAndApplication> getAppsOfUser(int? id)
        {
            return db.UsersAndApplications.Where(ua => ua.Profile.ProfileId == id && ua.Application.IsActive == true).ToList();
        }

       
    }
}
