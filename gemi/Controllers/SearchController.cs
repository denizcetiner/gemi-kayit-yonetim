using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using gemi.DAL;
using gemi.Entities;

namespace gemi.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetShipsByRef()
        {
            return View("SearchRef");
        }

        [HttpGet]
        public ActionResult GetShipsByName()
        {
            ViewBag.header = "İsim ile arama sonuçları";
            return View("SearchName");
        }

        [HttpPost]
        public ActionResult GetShipsByName(string ship_name)
        {
            ShipData shipData = new ShipData();
            TanimData tanimData = new TanimData();
            Dictionary<int, string> tanimlar = tanimData.GetTanimlar(); //id,string dict
            int ship_id = tanimlar.FirstOrDefault(x => x.Value == ship_name).Key;
            ViewBag.tanimlar = tanimlar;
            List<Ship> ships = shipData.GetShipsByName(ship_id);
            return View("SearchResults", ships);
        }

        [HttpGet]
        public ActionResult GetShipsByDate()
        {
            ViewBag.header = "Tarih ile arama sonuçları";
            return View("SearchDate");
        }

        [HttpGet]
        public ActionResult GetShipsByNameAndDate()
        {

            return View("SearchNameDate");
        }

        [HttpPost]
        public ActionResult GetShipsByDate(string begin, string end)
        {
            ShipData shipData = new ShipData();
            TanimData tanimData = new TanimData();
            Dictionary<int, string> tanimlar = tanimData.GetTanimlar(); //id,string dict
            ViewBag.tanimlar = tanimlar;
            List<Ship> ships = shipData.GetShipsBetweenDate(Convert.ToDateTime(begin + ",00:00:00").Date, Convert.ToDateTime(end + ",00:00:00").Date);

            return View("SearchResults",ships);
        }

        [HttpPost]
        public ActionResult GetShipsByNameAndDate(string ship_name, string begin, string end)
        {
            ShipData shipData = new ShipData();
            TanimData tanimData = new TanimData();
            Dictionary<int, string> tanimlar = tanimData.GetTanimlar();
            int ship_id = tanimlar.FirstOrDefault(x => x.Value == ship_name).Key;
            ViewBag.tanimlar = tanimlar;

            List<Ship> ships = shipData.GetShipsByDateAndName(ship_id, Convert.ToDateTime(begin + ",00:00:00").Date.AddDays(-1), Convert.ToDateTime(end + ",00:00:00").Date.AddDays(1));

            return View("SearchResults", ships);
        }
    }
}
