using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GVMap.Models;
using MongoDB.Bson;
using GVMap.Core.Repository.Markers;

namespace GVMap.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMarkerRepository repository;

        public HomeController(IMarkerRepository repository)
        {
            this.repository = repository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PutMarker(ObjectId? id)
        {
            if (id.HasValue)
            {
                MarkerModel result = repository.GetMarker(id.Value);
                return View("_PopupMarker", result);
            }

            return View("_PopupMarker");
        }

        public void UpsertMarker(ObjectId? id, string text, string imageUrl)
        {
            if (id.HasValue)
            {
                repository.UpdateMarker(id.Value, text, imageUrl);
            }
            else
            {
                repository.InsertMarker(text, imageUrl);
            }
        }
    }
}
