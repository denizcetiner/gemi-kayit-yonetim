﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using gemi.Entities;
using gemi.DAL;
using gemi.OtherMethods;
using System.Web.Security;

namespace gemi.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            if(Request.IsAuthenticated)
            {
                
                if (User.IsInRole("admin"))
                {
                    return View();
                }
                else
                {
                    //yeterli yetki yok
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["Message"] = "Önce giriş yapmanız gerekiyor";
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult UserPanel()
        {
            RolesData rolesData = new RolesData();
            UserData userData = new UserData();
            if (Request.IsAuthenticated)
            {
                if (User.IsInRole("admin"))
                {
                    List<User> users = userData.GetUsers();
                    List<Entities.Roles> roles = rolesData.GetRoles();

                    ViewBag.users = users;
                    ViewBag.roles = roles;

                    return View();
                }
                else
                {
                    TempData["Message"] = "Yeterli yetkiniz yok";
                    return RedirectToAction("Index", "View");
                }
            }
            else
            {
                TempData["Message"] = "Önce giriş yapmalısınız";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult AddUser(string username, string password, string role)
        {
            RolesData rolesData = new RolesData();
            if (Request.IsAuthenticated)
            {
                if(User.IsInRole("admin"))
                {
                UserData userData = new UserData();
                User user = new Entities.User();
                user.username = username;
                PasswordMethods pass = new PasswordMethods();
                user.password = pass.Hash(password);
                userData.AddUser(user);
                rolesData.AddUser(username,role);
                return View("Index");
                }
                else
                {
                   TempData["Message"] = "Yeterli yetkiniz yok";
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult SetRole(string username, string role)
        {
            RolesData rolesData = new RolesData();
            if (Request.IsAuthenticated)
            {
                if(User.IsInRole("admin"))
                {
                rolesData.SetRole(username, role);
                return View("Index");
                }
                else
                {
                    TempData["Message"] = "Yeterli yetkiniz yok";
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult RemoveUser(string username)
        {
            RolesData rolesData = new RolesData();
            if (Request.IsAuthenticated)
            {
                if (User.IsInRole("admin"))
                {
                    rolesData.RemoveUser(username);
                    UserData userData = new UserData();
                    userData.RemoveUser(username);
                    return View("Index");
                }
                else
                {
                    TempData["Message"] = "Yeterli yetkiniz yok";
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult ChangePassword(string username, string password)
        {
            RolesData rolesData = new RolesData();
            if (Request.IsAuthenticated)
            {
                if(User.IsInRole("admin"))
                {
                    UserData userData = new UserData();
                    userData.ChangePassword(username, password);
                    return View("Index");
                }
                else
                {
                    TempData["Message"] = "Yeterli yetkiniz yok";
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult EditUser(string username)
        {
            RolesData rolesData = new RolesData();
            if (Request.IsAuthenticated)
            {
                if(User.IsInRole("admin"))
                {
                    UserData userData = new UserData();
                    ViewBag.role = rolesData.GetRole(username);
                    ViewBag.username = username;
                    return View();
                }
                else
                {
                    TempData["Message"] = "Yeterli yetkiniz yok";
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult EditUser(string oldname, string newname,string password, string role)
        {
            RolesData rolesData = new RolesData();
            if (Request.IsAuthenticated)
            {
                if (User.IsInRole("admin"))
                {
                    UserData userData = new UserData();
                    PasswordMethods pass = new PasswordMethods();
                    password = pass.Hash(password);
                    rolesData.ChangeUserName(oldname, newname);
                    userData.ChangeName(oldname, newname);
                    userData.ChangePassword(newname, password);
                    rolesData.SetRole(newname, role);
                    return View("Index");
                }
                else
                {
                    TempData["Message"] = "Yeterli yetkiniz yok";
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        
    }
}
