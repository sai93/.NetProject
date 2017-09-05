using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorLogModel;

namespace LoadersAndLogic
{
    public class AppHandler
    {
        sjinkaDataBaseEntities db = new sjinkaDataBaseEntities();


        public List<Application> getApplications()
        {
            
           
           
                var res = db.Applications.ToList();
                return res;
            
           
        }

        public void addApp(Application app)
        {
           
                db.Applications.Add(app);
                db.SaveChanges();
            
        }

        public bool disableApp(Application app)
        {
            
                if (app != null)
                {
                    //db.Applications.Remove(app);
                    app.IsActive = false;
                    db.SaveChanges();
                    return true;
                }
                return false;
            
               
        }

        public void enableApp(int? id)
        {
           var App =  db.Applications.Single(ap => ap.ApplicationId == id);
            if(App != null)
            App.IsActive = !App.IsActive;
            db.SaveChanges();
           
        }

        public void disableApp(int? id)
        {
           
                db.Applications.Single(ap => ap.ApplicationId == id).IsActive = false;
                db.SaveChanges();
           
        }

        public void delete(int? id)
        {
           
                db.Applications.Remove(db.Applications.Single(ap => ap.ApplicationId == id));
                db.SaveChanges();
            
        }
    }
}
