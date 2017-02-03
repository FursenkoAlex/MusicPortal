using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicPortal.Models;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;
using static System.Web.Security.FormsAuthentication;


namespace MusicPortal.Controllers
{
    public class HomeController : Controller
    {
        private MusicPortalContext db = new MusicPortalContext();

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult SignIn(int i = 0)
        {
            ViewBag.Users = db.Users;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(User userModel)
        {
            var user = db.Users.FirstOrDefault(u => u.Login == userModel.Login);
            if (user != null)
            {
                string salt = user.Salt.Code;

                //солится
                string hash = FormsAuthentication.HashPasswordForStoringInConfigFile(salt + userModel.Password, "MD5");
                // string password = userModel.Password;

                //запись в сессию текущиго пользователя
                Session["userName"] = userModel.Login;
                // Session["userPassword"] = userModel.Password;
                string name = Session["userName"].ToString();
                //string pass = Session["userPassword"].ToString();
                var pass = db.Users.FirstOrDefault(p => p.Password == hash);


                //проверка если пользователь админ  
                if (name == "admin" && pass != null)
                {
                    return RedirectToAction("StartAdmin");
                }
                //проверка на соответствие имени и пароля
                //User isLoginTrue = db.Users.FirstOrDefault(u => u.Login == userModel.Login);
                var access = user.StatusUser;



                if (user?.Password.Equals(pass.Password) ?? false)
                {
                    //вход на пользовательскую страницу
                    if (access)
                        return RedirectToAction("Index", "UserPage");
                    else
                    {
                        ViewBag.Comment = "Your access is waiting confirmation";
                        return View();

                    }

                }

            }
            ViewBag.Comment = "Incorrect Login or Password";
            return View();
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(User userModel)
        {
            if (ModelState.IsValid)
            {

                byte[] saltbuf = new byte[16];
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                rng.GetBytes(saltbuf);
                StringBuilder sb = new StringBuilder(16);
                for (int i = 0; i < 16; i++)
                    sb.Append(string.Format("{0:X2}", saltbuf[i]));
                string salt = sb.ToString();
                string hash = HashPasswordForStoringInConfigFile(salt + userModel.Password, "MD5");
                userModel.Password = hash;
                userModel.Password2 = hash;
                userModel.Salt = new Salt() { Code = salt };

                db.Users.Add(userModel);
                db.SaveChanges();
                //ViewBag.Users = db.Users.ToList();
                ViewBag.Comment = "Request sent admin";
                return RedirectToAction("SignIn");
            }
            return View();
        }

        [HttpGet]
        public ActionResult StartAdmin()
        {
            return View();
        }




    }
}