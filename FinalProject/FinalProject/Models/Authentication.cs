
namespace FinalProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Security.Cryptography;
    using ErrorLogModel;

    public class Authentication
    {
        /// <summary>
        /// This is a SALT.. it should be "random" and complex, while it should not change over time.
        /// 
        /// Best practice is to generate a new salt for each user; in our case we do so by combidning the username w/ the salt, and w/ the password itself
        /// 
        /// This salt, as well as the logic in the HashPassword method should not be made public
        /// </summary>
        private const string SALT = "This.4i35i34890099+IsA Rand0m..<<>>//.,{)_@*$()&%&394802 98S alt390 @&$((*@()\":KFHDDF908r40932i;;efdjsklhfsd8oroy389ryhfsklddnfds218()*$()@^$*()@*YF";
        private static sjinkaDataBaseEntities db = new sjinkaDataBaseEntities();
        /// <summary>
        /// Hashes the password for the user, and returns the hashed password
        /// </summary>
        private static string HashPassword(string userName, string nonHashedPassword)
        {
            
            if (string.IsNullOrEmpty(userName)) { throw new ArgumentNullException("user.UserName"); }
            if (string.IsNullOrEmpty(nonHashedPassword)) { throw new ArgumentNullException("user.Password"); }

            string result = string.Empty;

            // we are using the SHA512 algorithm. 
            // please read links above to learn about available options, strengths and downsides of different algorithms available
            HashAlgorithm hash = new SHA512CryptoServiceProvider();

            // we break the salt into 3 parts, and then combine it w/ the username as well as the password to make hashtables much more difficult to use
            int thirdOfALength = SALT.Length / 3;

            string stringToHash = SALT.Substring(0, thirdOfALength)
                + userName + SALT.Substring(thirdOfALength - 1, thirdOfALength)
                + nonHashedPassword + SALT.Substring(thirdOfALength * 2 - 1);

            // do the hashing (convert string to bytes, do the hashing, convert hashed bytes back to string
            byte[] bytesToHash = Encoding.UTF8.GetBytes(stringToHash);
            byte[] hashedBytes = hash.ComputeHash(bytesToHash);
            result = Convert.ToBase64String(hashedBytes);

            return result;
        }

        /// <summary>
        /// Method authenticates the user.. returns true if the user's credentials are correct, and false if they are not
        /// </summary>
        public static Profile AuthenticateUser(string Email, string nonHashedPassword)
        {
            //bool result = false;

            try
            {
                string hashedPassword = HashPassword(Email, nonHashedPassword);

                //List<UserData> savedUserInfo = PersistingUserData.GetUserData();

                // we have to match exactly 1 user item
                
               if(db.Profiles.Count(x => x.Password == hashedPassword && x.Email == Email) == 1)
                {
                    return db.Profiles.Single(x => x.Password == hashedPassword && x.Email == Email && x.IsActive == true);
                }
            }
            catch
            {
                /* need to log */
                
            }

            return null;
        }

        /// <summary>
        /// Registers the user (saves the user into the XML file)
        /// </summary>
        public static bool RegisterUser(Profile profile)
        {
            bool result = false;

            try
            {
                string hashedPassword = HashPassword(profile.Email, profile.Password);
               // List<UserData> savedUserInfo = PersistingUserData.GetUserData();

                // if there are no existing users already saved.. save it
                if (!db.Profiles.Any(x => x.Email == profile.Email))
                {
                    profile.Password = hashedPassword;
                    db.Profiles.Add(profile);
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                /* need to log */
                result = false;
            }

            return result;
        }
    }
}