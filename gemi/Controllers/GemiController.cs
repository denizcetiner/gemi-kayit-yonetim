using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using gemi.Entities;
using gemi.DAL;
using gemi.OtherMethods;

namespace gemi.Controllers
{
    public class GemiController : Controller
    {
        //
        // GET: /Gemi/

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index","Home");
            }
        }

        [HttpGet]
        public ActionResult GetShip(string ref_id)
        {
            ShipData shipData = ShipData.GetShipData();
            Ship ship = new Ship();
            ship = shipData.GetShip(ref_id);

            if (ship.refId == null)
            {
                TempData["Message"] = "Bulunamadı";
                return RedirectToAction("Index","Home");
            }
            else
            {
                TanimData tanimData = TanimData.GetTanimData();
                Dictionary<int,string> tanimlar =  tanimData.GetTanimlar();
                ViewBag.tanimlar = tanimlar;
                ShipUrlData shipUrlData = ShipUrlData.GetShipUrlData();
                List<ShipUrl> shipPhotos = shipUrlData.GetPhotos(ref_id);
                ViewBag.Photos = shipPhotos;
                ViewBag.Ship = ship;
                return View();
            }
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        [HttpGet]
        public ActionResult Upload()
        {
            if (Request.IsAuthenticated)
            {
                UserData userData = UserData.GetUserData();
                RolesData rolesData = RolesData.GetRolesData();
    
                if (User.IsInRole("admin") || (User.IsInRole("superuser")))
                {
                    TanimData tanimData = TanimData.GetTanimData();
                    ViewBag.Tanimlar = tanimData.GetTanimlar();
                    return View();
                }
                else
                {
                    TempData["Message"] = "Yükleme izniniz yok";
                    return RedirectToAction("Index","Home");
                }
            }
            else
            {
                TempData["Message"] = "Yükleme izni olan bir hesap ile giriş yapmalısınız";
                return RedirectToAction("Index","Home");
            }
        }

        [HttpPost]
        public ActionResult Upload(string ref_id, int ship_id, string time, string description, HttpPostedFileBase[] files) //files=resim olacak
        {
            
            if (Request.IsAuthenticated && (User.IsInRole("superuser") || User.IsInRole("admin")))
            {
                ShipData shipData = ShipData.GetShipData();
                if (!shipData.CheckIfExists(ref_id))
                {
                    UpdateUploadShip(ref_id, ship_id, description, time, files, "upload");

                    TempData["Message"] = "Kayıt eklendi";
                    return RedirectToAction("Records", "Gemi");
                }
                else
                {
                    TempData["Message"] = "Aynı referanslı gemi zaten var";
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["Message"] = "Yetkiniz yok";
                return RedirectToAction("Index", "Home");
            }
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        [HttpGet]
        public ActionResult EditShip(string ref_id)
        {
            ShipData shipData = ShipData.GetShipData();

            string createdBy = shipData.GetCreatedBy(ref_id);

            if (User.Identity.Name == createdBy || User.IsInRole("admin"))
            {

                Ship ship = new Ship();
                ship = shipData.GetShip(ref_id);
                ShipUrlData shipUrlData = ShipUrlData.GetShipUrlData();
                List<ShipUrl> shipPhotos = shipUrlData.GetPhotos(ref_id);
                ViewBag.Photos = shipPhotos;

                TanimData tanimData = TanimData.GetTanimData();
                ViewBag.tanimlar = tanimData.GetTanimlar();

                return View(ship);
            }
            else
            {
                TempData["Message"] = "Bu kayıt başkası tarafından oluşturulduğu için düzenleyemezsiniz";
                return RedirectToAction("Index","Home");
            }
        }

        [HttpPost]
        public ActionResult EditShip(string ref_id, int ship_id, string description, string time, HttpPostedFileBase[] files)
        {
            UpdateUploadShip(ref_id, ship_id, description, time, files, "update");

            TempData["Message"] = "Kayıt güncellendi";
            TempData["Redirect"] = "/Gemi/GetShip?ref_id="+ref_id;
            return RedirectToAction("Index","Home");
        }

        public bool UpdateUploadShip(string ref_id, int ship_id, string description, string time, HttpPostedFileBase[] files, string choice)
        {
            string savingPath = Server.MapPath("~/Content/images/");
            System.IO.Directory.CreateDirectory(savingPath);

            Ship ship = new Ship();
            ship.refId = ref_id;
            ship.shipId = ship_id;
            ship.time = Convert.ToDateTime(time + ",00:00:00").Date;
            ship.description = description;
            ship.createdBy = User.Identity.Name;
            ship.createdPc = Request.UserHostName;
            ship.createdDatetime = DateTime.Now;

            //System.IO.Directory.CreateDirectory(Server.MapPath("~/Content/images"));
            ShipUrlData shipUrlData = ShipUrlData.GetShipUrlData();
            ShipData shipData = ShipData.GetShipData();

            ImageResize resizer = new ImageResize();
            FileMethods filer = new FileMethods();

            System.Drawing.Image photo;
            System.Drawing.Image preview;
            if (choice == "upload")
            {
                shipData.AddGemi(ship);
            }

            else if (choice == "update")
            {
                shipData.UpdateShip(ship);
            }

            foreach (HttpPostedFileBase file in files)
            {
                if (file == null) { continue; }
                if (shipUrlData.CheckDuplicateUrl(ref_id, file.FileName, Server.MapPath("~/Content/images/")))
                {
                    if (filer.CompareSizeOfFile(file, Server.MapPath("~/Content/images/" + ref_id + "_" + file.FileName)))
                    {
                        continue;
                    }
                }
                ShipUrl shipUrl = new ShipUrl();

                photo = filer.FileToImage(file);
                preview = resizer.Resize(photo, 320, 240);
                byte[] previewByte = filer.ImageToBytes(preview);

                shipUrl.refId = ref_id;
                shipUrl.preview = previewByte;

                string filename = savingPath + ref_id + "_" + file.FileName;

                /*if (filer.FileExists("", filename))
                {
                    while (filer.FileExists("", filename))
                    {
                        filename = filer.renameFile("", filename);
                    }
                }*/ //başına ref_id eklendiğinden kullanımına gerek kalmadı

                filer.SaveFile(photo, "", filename); //filename'e zaten dosya adından öncesi eklendi 
                shipUrl.imageUrl = filename;

                shipUrlData.AddShipUrl(shipUrl);
                photo.Dispose();
                preview.Dispose();
            }
            

            return true;
        }
        [HttpPost]
        public ActionResult DeletePicture(int photo_id, string ref_id)
        {
            ShipData shipData = ShipData.GetShipData();

            if (User.Identity.Name == shipData.GetCreatedBy(ref_id))
            {
                ShipUrlData shipUrlData = ShipUrlData.GetShipUrlData();

                FileMethods filer = new FileMethods();
                filer.DeleteFile(shipUrlData.GetFilePath(photo_id));


                shipUrlData.DeletePicture(photo_id);
                bool result = true;
                if (Request.IsAjaxRequest())
                {
                    return Json(result);
                }

                TempData["Message"] = "Fotoğraf silindi";
                return RedirectToAction("EditShip", new { ref_id = ref_id });

            }
            else
            {
                TempData["Message"] = "Silmek istediğiniz fotoğraf sizin tarafınızdan yüklenmedi";
                return RedirectToAction("Index","Home");
            }
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        [HttpGet]
        public ActionResult Records()
        {
            ShipData shipData = ShipData.GetShipData();
            TanimData tanimData = TanimData.GetTanimData();
            Dictionary<int, string> tanimlar = tanimData.GetTanimlar();
            ViewBag.tanimlar = tanimlar;
            List<Ship> Records = shipData.GetRecords(User.Identity.Name);
            return View(Records);
        }
    }
}
