using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorLogModel;


namespace LoadersAndLogic
{
    public class ProfHandler
    {
        sjinkaDataBaseEntities db = new sjinkaDataBaseEntities();
        public void addProfile(Profile profile)
        {
            db.Profiles.Add(profile);
            db.SaveChanges();
        }

        public bool checkUser(int SessionId)
        {
            return db.Profiles.Any(pf => pf.ProfileId == SessionId && pf.IsAdmin == false);
        }

        public bool checkAdmin(int SessionId)
        {
            return db.Profiles.Any(pf => pf.ProfileId == SessionId && pf.IsAdmin == true);
        }

        public List<Profile> getUsers()
        {
            
            return db.Profiles.Where(pf => pf.IsAdmin != true).ToList();
        }
        public Profile getProfile(UsersAndApplication appuser)
        {
            return db.Profiles.Single(ua => ua.ProfileId == appuser.UserId);
        }
        public Profile getProfile(int? id)
        {
            return db.Profiles.Single(ua => ua.ProfileId == id);
        }

        public List<Profile> getProfiles(int SessionId)
        {
            return db.Profiles.Where(pf => pf.ProfileId != SessionId).ToList();
        }
        public void makeAdmin(int? id)
        {
            var prof = db.Profiles.Single(profile => profile.ProfileId == id);
            if(prof != null)
            {
                prof.IsAdmin = !prof.IsAdmin;
            }
            db.SaveChanges();
        }
        public void makeActive(int? id)
        {
            var prof = db.Profiles.Single(profile => profile.ProfileId == id);
            if (prof != null)
            {
                prof.IsActive = !prof.IsActive;
            }
            db.SaveChanges();
        }

        public void UpdateLastLogin(int? id)
        {
            var profile = db.Profiles.Single(pf => pf.ProfileId == id);
            profile.LastLoginDate = DateTime.Now;
            db.SaveChanges();
        }
    }
}
