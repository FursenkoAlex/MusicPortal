using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using static System.Web.Security.FormsAuthentication;


namespace MusicPortal.Models
{
    public class MusicPortalInitializer : CreateDatabaseIfNotExists<MusicPortalContext>
    {
        protected override void Seed(MusicPortalContext db)
        {
            byte[] saltbuf = new byte[16];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(saltbuf);
            StringBuilder sb = new StringBuilder(16);
            for (int i = 0; i < 16; i++)
                sb.Append(string.Format("{0:X2}", saltbuf[i]));
            string salt = sb.ToString();
            string hash = HashPasswordForStoringInConfigFile(salt + "admin", "MD5");
            

            db.Users.Add(new User { Login = "admin", Password = hash, Password2 = hash,
                Salt  = new Salt {Code = salt }, StatusUser = true, StatusAdmin = true });

            db.Ganers.Add(new Ganre { GanreName = "Pop" });
            db.Ganers.Add(new Ganre { GanreName = "Rock" });
            db.Ganers.Add(new Ganre { GanreName = "Disco" });
            db.Ganers.Add(new Ganre { GanreName = "Soul" });
            base.Seed(db);
        }
    }
}