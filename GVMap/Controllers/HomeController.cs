using System;
using System.Collections.Generic;
using System.IO;
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

        public ActionResult GetImage(string id)
        {
            return File(repository.GetMarker(new ObjectId(id)).Image, "image/*");
        }

        public JsonResult GetMarkers()
        {
            var markers = repository.GetAllMarkers();

            return Json(markers.Select(m => new { m.Description, m.Coordinates, Id = m.Id.ToString() }), JsonRequestBehavior.AllowGet);
        }


        public ActionResult UpsertMarker(ObjectId? id, string coordinates, string description, HttpPostedFileBase file)
        {
            using (var ms = new MemoryStream())
            {
                file.InputStream.CopyTo(ms);

                if (id.HasValue)
                {
                    var model = new MarkerModel
                    {
                        Id = id.Value,
                        Coordinates = coordinates,
                        Description = description,
                        Image = ms.ToArray()
                    };

                    repository.UpdateMarker(model);
                }
                else
                {
                    var model = new MarkerModel
                    {
                        Coordinates = coordinates,
                        Description = description,
                        Image = ms.ToArray()
                    };

                    repository.InsertMarker(model);
                }
            }

            return RedirectToAction("Index");
        }
    }
}
