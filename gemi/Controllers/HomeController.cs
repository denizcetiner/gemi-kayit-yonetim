using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using gemi.DAL;
using gemi.Entities;
using gemi.OtherMethods;
using System.Web.Security;

namespace gemi.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"];
            }
            if(TempData["Redirect"] != null)
            {
                ViewBag.redirect = TempData["Redirect"];
            }
            bool result = Request.IsAuthenticated;
            return View("Index");
        }

        [HttpPost]
        public ActionResult Login(string username, string remember, string password)
        {
            User user = new User();
            user.username = username;
            PasswordMethods pass = new PasswordMethods();
            user.password = pass.Hash(password);

            UserData userData = UserData.GetUserData();
            if (Request.IsAuthenticated)
            {
                TempData["Message"] = "Zaten giriş yapmışsınız";
                return RedirectToAction("Index");
            }
            else
            {
                if (userData.LoginUser(user))
                {
                    if (remember == "on")
                    {
                        HttpCookie hc = new HttpCookie("username");
                        hc.Value = username;
                        Response.Cookies.Add(hc);
                    }
                    else if (remember == null)
                    {
                        if (Request.Cookies["username"] != null)
                        {
                            HttpCookie hc = new HttpCookie("username");
                            hc.Expires = DateTime.Now.AddDays(-1);
                            Response.Cookies.Add(hc);
                        }
                    }

                    RolesData rolesData = RolesData.GetRolesData();
                    string role = rolesData.GetRole(user.username);

                    System.Web.Security.FormsAuthenticationTicket ticket = new System.Web.Security.FormsAuthenticationTicket(
                        1,
                        user.username,
                        DateTime.Now,
                        DateTime.Now.AddMinutes(30),
                        false,
                        role,
                        System.Web.Security.FormsAuthentication.FormsCookiePath);

                    string EncryptedTicket = System.Web.Security.FormsAuthentication.Encrypt(ticket);

                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, EncryptedTicket);
                    cookie.HttpOnly = true;
                    Response.Cookies.Add(cookie);

                    //System.Web.Security.FormsAuthentication.SetAuthCookie(user.username, false);
                    TempData["Message"] = "Giriş başarılı";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Message"] = "Yanlış kullanıcı adı veya şifre";
                    return RedirectToAction("Index");
                }
            }
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            if (Request.IsAuthenticated)
            {
                return View();
            }
            else
            {
                TempData["Message"] = "Giriş yapmamışsınız";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult ChangePassword(string oldpass,string newpass1,string newpass2)
        {
            if (Request.IsAuthenticated)
            {
                if(newpass1==newpass2)
                {
                    PasswordMethods pass = new PasswordMethods();
                    UserData userData = UserData.GetUserData();
                    User user = new User();
                    user.username = User.Identity.Name;
                    user.password = pass.Hash(oldpass);
                    if (userData.LoginUser(user))
                    {
                        userData.ChangePassword(User.Identity.Name, pass.Hash(newpass1));
                        TempData["Message"] = "Şifreniz başarıyla değiştirildi";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Message"] = "Eski şifrenizi yanlış girdiniz";
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    TempData["Message"] = "Şifreler birbiriyle uyuşmuyor";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["Message"] = "Giriş yapmamışsınız";
                return RedirectToAction("Index");
            }
        }

        public ActionResult Logout()
        {
            if (!Request.IsAuthenticated)
            {
                TempData["Message"] = "Zaten giriş yapmamışsınız";
                return RedirectToAction("Index");
            }
            else
            {
                System.Web.Security.FormsAuthentication.SignOut();
                TempData["Message"] = "Çıkış başarılı";
                return RedirectToAction("Index");
            }
        }
        [OutputCache(Duration=1200)]
        public ActionResult ShowCurrency()
        {
            XMLMethods xml = new XMLMethods();
            Dictionary<string, string> currency = xml.GetCurrencies();
            return PartialView("_Currency", currency);
        }
        //[OutputCache(Duration=3600)]
        public ActionResult ShowForecast()
        {
            XMLMethods xml = new XMLMethods();
            Dictionary<string, string> forecast = xml.GetForecastYahoo();
            return PartialView("_Forecast", forecast);
        }
        public PartialViewResult SideBar()
        {
            return PartialView("_Sidebar");
        }
        public PartialViewResult Account()
        {
            return PartialView("_Profile");
        }
    }
}
