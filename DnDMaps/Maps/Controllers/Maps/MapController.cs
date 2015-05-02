using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maps.Models;

namespace Maps.Controllers
{
    public class MapController : Controller
    {
        private DND_MAPS_Entities db = new DND_MAPS_Entities();

        //
        // GET: /Map/

        public ActionResult Index()
        {
            return View(db.Maps.ToList());
        }

        //
        // GET: /Map/Details/5

        public ActionResult Details(int id = 0)
        {
            Map map = db.Maps.Find(id);
            if (map == null)
            {
                return HttpNotFound();
            }
            return View(map);
        }

        //
        // GET: /Map/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Map/Create

        [HttpPost]
        public ActionResult Create(Map map)
        {
            if (ModelState.IsValid)
            {
                db.Maps.Add(map);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(map);
        }

        //
        // GET: /Map/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Map map = db.Maps.Find(id);
            if (map == null)
            {
                return HttpNotFound();
            }
            return View(map);
        }

        //
        // POST: /Map/Edit/5

        [HttpPost]
        public ActionResult Edit(Map map)
        {
            if (ModelState.IsValid)
            {
                db.Entry(map).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(map);
        }

        //
        // GET: /Map/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Map map = db.Maps.Find(id);
            if (map == null)
            {
                return HttpNotFound();
            }
            return View(map);
        }

        //
        // POST: /Map/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Map map = db.Maps.Find(id);
            db.Maps.Remove(map);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}