using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GVMap.Models;

namespace GVMap.Controllers
{
    public class HomeController : Controller
    {
        private readonly MapRepository repository;

        public HomeController(MapRepository repository)
        {
            this.repository = repository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PutMarker(long? id)
        {
            if (id.HasValue)
            {
                MarkerModel result = repository.getMarkerInfo(id);
                return View("_PopupMarker", result);
            }

            return View("_PopupMarker");
        }
    }
}
