using BLL;
using BOL;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MovieHubUI.Controllers
{
    public class MovieController : Controller
    {
        private MovieBS movieBs;
        ImageService imageService = new ImageService();

        public MovieController()
        {
            movieBs = new MovieBS();
        }

        public ActionResult Index(int? page)
        {
            var MovieBS = movieBs.GetAll();
            return View(MovieBS.ToList().ToPagedList(page ?? 1, 5));
        }

        public ActionResult Details(int id)
        {
            tbl_Movie tbl_Movie = movieBs.GetById(id);
            if (tbl_Movie == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Movie);
        }

        // GET: Movie
        [HttpGet]
        public ActionResult Create()
        {
            GetSelectLists();
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(HttpPostedFileBase photo, tbl_Movie objMovie, FormCollection frmCollection)
        {

            if (ModelState.IsValid)
            {

                //upload image to azure blob
                objMovie.Poster = photo.FileName;
                await Upload(photo);
                //If actors added, get their Ids.
                String[] arrActorId = new String[] { };
                if (!String.IsNullOrEmpty(frmCollection["ActorId"]))
                {
                    arrActorId = frmCollection["ActorId"].Split(',');
                }

                //Insert the record.
                movieBs.Insert(objMovie, arrActorId);
                return RedirectToAction("Index");
            }
            GetSelectLists();
            return View(objMovie);

        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieDbEntities db = new MovieDbEntities();
            tbl_Movie tbl_Movie = db.tbl_Movie.Find(id);

            if (tbl_Movie == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProducerId = new SelectList(db.tbl_Producer.OrderBy(x => x.Name), "ProducerId", "Name", tbl_Movie.ProducerId);
            IEnumerable<tbl_ActorMovie> tblActorIds = db.tbl_Movie.Local[0].tbl_ActorMovie;
            int[] arr = tblActorIds.Select(x => x.ActorId).ToArray();
            ViewBag.arrSelectedActors = arr;
            ViewBag.ActorId = new MultiSelectList(db.tbl_Actor.OrderBy(x => x.Name), "ActorId", "Name", arr.ToArray());
            ViewBag.SelectedActorId = arr;
            return View(tbl_Movie);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(HttpPostedFileBase photo, tbl_Movie objMovie, FormCollection frmCollection)
        {

            if (ModelState.IsValid)
            {               

                //If no new photo uploaded, take the old one . Else save the new one to cloud.
                if (photo != null)
                {
                    objMovie.Poster = photo.FileName;
                    await Upload(photo);
                }
                else
                {
                    objMovie.Poster = frmCollection["hdnFileUpload"];
                }

                //save actors.
                String[] arrActorId = new String[] { };
                if (!String.IsNullOrEmpty(frmCollection["ActorId"]))
                {
                    arrActorId = frmCollection["ActorId"].Split(',');
                }
                movieBs.Update(objMovie, arrActorId);
                return RedirectToAction("Index");
            }
            MovieDbEntities db = new MovieDbEntities();
            ViewBag.ProducerId = new SelectList(db.tbl_Producer.OrderBy(x => x.Name), "ProducerId", "Name", objMovie.ProducerId);
            ViewBag.ActorId = new SelectList(db.tbl_Actor.OrderBy(x => x.Name), "ActorId", "Name");
            return View(objMovie);

        }

        // GET: Actor/Delete/5
        public ActionResult Delete(int id)
        {

            tbl_Movie tbl_Movie = movieBs.FindDelete(id);
            if (tbl_Movie == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Movie);


        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int Id)
        {
            try
            {
                movieBs.Delete(Id);
                TempData["Msg"] = "Deleted successfully";
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                TempData["Msg"] = "Delete failed: " + ex.Message;
                return RedirectToAction("Index");

            }
        }

        //Gets the producer and actor list
        public void GetSelectLists()
        {
            MovieDbEntities db = new MovieDbEntities();
            ViewBag.ProducerId = new SelectList(db.tbl_Producer.OrderBy(x => x.Name), "ProducerId", "Name");
            ViewBag.ActorId = new SelectList(db.tbl_Actor.OrderBy(x => x.Name), "ActorId", "Name");
        }

        [HttpPost]
        public async Task<ActionResult> Upload(HttpPostedFileBase photo)
        {
            var imageUrl = await imageService.UploadImageAsync(photo);
            TempData["LatestImage"] = imageUrl.ToString();
            return RedirectToAction("LatestImage");
        }

        public ActionResult LatestImage()
        {
            var latestImage = string.Empty;
            if (TempData["LatestImage"] != null)
            {
                ViewBag.LatestImage = Convert.ToString(TempData["LatestImage"]);
            }

            return View();
        }

    }
}