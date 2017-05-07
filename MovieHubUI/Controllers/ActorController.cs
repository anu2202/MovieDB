using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BOL;

namespace MovieHubUI.Models
{
    public class ActorController : Controller
    {
        private MovieDbEntities db = new MovieDbEntities();

        // GET: Actor
        public ActionResult Index()
        {
            return View(db.tbl_Actor.ToList());
        }

        // GET: Actor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Actor tbl_Actor = db.tbl_Actor.Find(id);
            if (tbl_Actor == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Actor);
        }

        // GET: Actor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Actor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ActorId,Name,Sex,DOB,Bio")] tbl_Actor tbl_Actor)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Actor.Add(tbl_Actor);
                db.SaveChanges();
                return RedirectToAction("Create", "Movie");
            }

            return View(tbl_Actor);
        }

        // GET: Actor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Actor tbl_Actor = db.tbl_Actor.Find(id);
            if (tbl_Actor == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Actor);
        }

        // POST: Actor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ActorId,Name,Sex,DOB,Bio")] tbl_Actor tbl_Actor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Actor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_Actor);
        }

        // GET: Actor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Actor tbl_Actor = db.tbl_Actor.Find(id);
            if (tbl_Actor == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Actor);
        }

        // POST: Actor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Actor tbl_Actor = db.tbl_Actor.Find(id);
            db.tbl_Actor.Remove(tbl_Actor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
