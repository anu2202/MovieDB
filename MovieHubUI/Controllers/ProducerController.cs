using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BOL;

namespace MovieHubUI.Controllers
{
    public class ProducerController : Controller
    {
        private MovieDbEntities db = new MovieDbEntities();

        // GET: Producer
        public ActionResult Index()
        {
            return View(db.tbl_Producer.ToList());
        }

        // GET: Producer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Producer tbl_Producer = db.tbl_Producer.Find(id);
            if (tbl_Producer == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Producer);
        }

        // GET: Producer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Producer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProducerId,Name,DOB,Bio,Sex")] tbl_Producer tbl_Producer)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Producer.Add(tbl_Producer);
                db.SaveChanges();
                return RedirectToAction("Create", "Movie");
            }

            return View(tbl_Producer);
        }

        // GET: Producer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Producer tbl_Producer = db.tbl_Producer.Find(id);
            if (tbl_Producer == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Producer);
        }

        // POST: Producer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProducerId,Name,DOB,Bio,Sex")] tbl_Producer tbl_Producer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Producer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_Producer);
        }

        // GET: Producer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Producer tbl_Producer = db.tbl_Producer.Find(id);
            if (tbl_Producer == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Producer);
        }

        // POST: Producer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Producer tbl_Producer = db.tbl_Producer.Find(id);
            db.tbl_Producer.Remove(tbl_Producer);
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
