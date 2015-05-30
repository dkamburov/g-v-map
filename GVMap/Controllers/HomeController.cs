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

            return Json(markers.Select(m => new { m.Description, m.Coordinates, Id = m.Id.ToString(), User = m.User }), JsonRequestBehavior.AllowGet);
        }


        public ActionResult UpsertMarker(string id, string user, string coordinates, string description, HttpPostedFileBase file)
        {
            using (var ms = new MemoryStream())
            {
                if (file != null)
                {
                    file.InputStream.CopyTo(ms);
                }

                if (!string.IsNullOrEmpty(id))
                {
                    var model = new MarkerModel
                    {
                        Id = new ObjectId(id),
                        Coordinates = coordinates,
                        Description = description,
                        User = user,
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
                        User = user,
                        Image = ms.ToArray()
                    };

                    repository.InsertMarker(model);
                }
            }

            return RedirectToAction("Index");
        }
    }
}
