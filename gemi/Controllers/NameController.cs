using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using gemi.DAL;
using gemi.Entities;
using gemi.OtherMethods;

namespace gemi.Controllers
{
    public class NameController : Controller
    {
        //
        // GET: /Name/
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Index()
        {
            if(Request.IsAuthenticated)
            {
                if (User.IsInRole("admin"))
                {
                    TanimData tanimData = new TanimData();
                    List<Tanimlar> tanimlar = tanimData.GetAllTanimlar();
                    ViewBag.tanimlar = tanimlar;

                    return View();
                }
                else
                {
                    TempData["Message"] = "Yetkiniz yok";
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["Message"] = "Yetkiniz yok";
                return RedirectToAction("Index","Home");
            }
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        [HttpGet]
        public ActionResult NewName()
        {
            if (Request.IsAuthenticated)
            {
                if(User.IsInRole("admin"))
                {
                    TanimData tanimData = new TanimData();
                    Dictionary<int,string> tanimlar = tanimData.GetTanimlar();
                    ViewBag.tanimlar = tanimlar;
                    return View();
                }
                else
                {
                    TempData["Message"] = "Yeterli yetkiniz yok";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["Message"] = "Giriş yapmanız gerekiyor";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult NewName(string shipname)
        {
            if (Request.IsAuthenticated)
            {
                if (User.IsInRole("admin"))
                {
                    TanimData tanimData = new TanimData();
                    if (!tanimData.CheckIfExists(shipname))
                    {
                        tanimData.TanimEkle(shipname);
                        TempData["Message"] = "Yeni gemi başarıyla eklendi";
                        return RedirectToAction("NewName");
                    }
                    else
                    {
                        TempData["Message"] = "Aynı isimde gemi var";
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    TempData["Message"] = "Yeterli yetkiniz yok";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["Message"] = "Yeterli yetkiniz yok";
                return RedirectToAction("Index");
            }
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        [HttpGet]
        public ActionResult DeleteName()
        {
            if (Request.IsAuthenticated)
            {
                if (User.IsInRole("admin"))
                {
                    TanimData tanimData = new TanimData();
                    Dictionary<int,string> tanimlar = tanimData.GetTanimlar();
                    return View(tanimlar);
                }
                else
                {
                    TempData["Message"] = "Yeterli yetkiniz yok";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["Message"] = "Yeterli yetkiniz yok";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult DeleteName(int tanim_id)
        {
            if (Request.IsAuthenticated)
            {
                if (User.IsInRole("admin"))
                {
                    TempData["Message"] = "Bir gemiyi silmek üzeresiniz.Devam ederseniz bu gemiyi içeren kayıtlar da silinecektir";
                    return RedirectToAction("Continue", new { tanim_id = tanim_id });
                }
                else
                {
                    TempData["Message"] = "Yeterli yetkiniz yok";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["Message"] = "Yeterli yetkiniz yok";
                return RedirectToAction("Index");
            }
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        [HttpGet]
        public ActionResult Continue(int tanim_id)
        {
            if (Request.IsAuthenticated)
            {
                if (User.IsInRole("admin"))
                {
                    return View(tanim_id as object);
                }
                else
                {
                    TempData["Message"] = "Yeterli yetkiniz yok";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["Message"] = "Yeterli yetkiniz yok";
                return RedirectToAction("Index");
            }
        }

        
        [HttpPost]
        [ActionName("Continue")]
        public ActionResult ContinuePost(int tanim_id)
        {
            if (Request.IsAuthenticated)
            {
                if(User.IsInRole("admin"))
                {
                FileMethods filer = new FileMethods();
                TanimData tanimData = new TanimData();
                ShipData shipData = new ShipData();
                ShipUrlData shipUrlData = new ShipUrlData();
                tanimData.DeleteTanimById(tanim_id);
                
                List<string> references = shipData.GetShipReferencesOfName(tanim_id);
                shipData.DeleteShips(tanim_id);
                List<string> filenames = new List<string>();

                foreach(string reference in references)
                {
                    filenames.AddRange(shipUrlData.GetFilePaths(reference));
                }

                foreach (string filepath in filenames)
                {
                    filer.DeleteFile(filepath);
                }

                foreach (string i in references)
                {
                    shipUrlData.DeletePicturesOfShip(i);
                }

                if (Request.IsAjaxRequest())
                {
                    return Json(true);
                }

                TempData["Message"] = "Silme başarılı";
                return RedirectToAction("Index");
                }
                else
                {
                    TempData["Message"] = "Yetkiniz yok";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["Message"] = "Yetkiniz yok";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult EditName(int tanimId)
        {
            if (Request.IsAuthenticated && User.IsInRole("admin"))
            {
                TanimData tanimData = new TanimData();
                Tanimlar tanim = new Tanimlar();
                tanim.tanimId = tanimId;
                tanim.tanim = tanimData.getTanim(tanimId);
                return View(tanim);
            }
            else
            {
                return RedirectToAction("Index","Home");
            }
        }
        [HttpPost]
        public ActionResult EditName(int tanimId, string oldname, string newname)
        {
            if (Request.IsAuthenticated && User.IsInRole("admin"))
            {
                TanimData tanimData = new TanimData();
                if (!tanimData.CheckIfExists(newname))
                {
                    tanimData.EditName(tanimId, oldname, newname);
                    return RedirectToAction("Index", "Name");
                }
                else
                {
                    TempData["Message"] = "Bu isimde bir gemi var";
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
