using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maps.Models;
using System.Configuration;
using System.IO;

namespace Maps.Controllers.Characters
{
    public class CharacterController : Controller
    {
        private Entities db = new Entities();

        //
        // GET: /Character/

        public ActionResult Index()
        {

            try
            {

                var characters = db.Characters.Include(c => c.Player);
                return View(characters.ToList());
            }
            catch (Exception)
            {
                return View();
            }
        }


        //
        // GET: /Character/Create

        public ActionResult Create()
        {
            ViewBag.Player_ID = new SelectList(db.Players, "Player_ID", "Name");
            return View();
        }

        //
        // POST: /Character/Create

        [HttpPost]
        public ActionResult Create(Character character, HttpPostedFileBase Avatar_IMG_Upload)
        {
            if (ModelState.IsValid)
            {


                if (Avatar_IMG_Upload != null && Avatar_IMG_Upload.ContentLength > 0)
                {

                    //generate a guid, store the file upload as this name...
                    Guid fileName = Guid.NewGuid();

                    string targetFolder = HttpContext.Server.MapPath(ConfigurationManager.AppSettings["UPLOADED_MAP_IMAGES_RELATIVE_PATH"]);
                    string targetPath = Path.Combine(targetFolder, fileName.ToString());
                    targetPath += "." + Avatar_IMG_Upload.FileName.Split('.')[1];

                    Avatar_IMG_Upload.SaveAs(targetPath); //save the uploaded file to the server's file system...

                    character.Avatar_IMG = fileName + "."  + Avatar_IMG_Upload.FileName.Split('.')[1]; //store the path of uploaded file in the database entity.
                }

                db.Characters.Add(character);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Player_ID = new SelectList(db.Players, "Player_ID", "Name", character.Player_ID);
            return View(character);
        }

        //
        // GET: /Character/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Character character = db.Characters.Find(id);
            if (character == null)
            {
                return HttpNotFound();
            }
            ViewBag.Player_ID = new SelectList(db.Players, "Player_ID", "Name", character.Player_ID);
            return View(character);
        }

        //
        // POST: /Character/Edit/5

        [HttpPost]
        public ActionResult Edit(Character character, HttpPostedFileBase Avatar_IMG_Upload)
        {
            if (ModelState.IsValid)
            {

                if (Avatar_IMG_Upload != null && Avatar_IMG_Upload.ContentLength > 0)
                {

                    //generate a guid, store the file upload as this name...
                    Guid fileName = Guid.NewGuid();

                    string targetFolder = HttpContext.Server.MapPath(ConfigurationManager.AppSettings["UPLOADED_MAP_IMAGES_RELATIVE_PATH"]);
                    string targetPath = Path.Combine(targetFolder, fileName.ToString());
                    targetPath += "." + Avatar_IMG_Upload.FileName.Split('.')[1];

                    Avatar_IMG_Upload.SaveAs(targetPath); //save the uploaded file to the server's file system...

                    character.Avatar_IMG = fileName + "." + Avatar_IMG_Upload.FileName.Split('.')[1]; //store the path of uploaded file in the database entity.
                }
                else
                {
                    character.Avatar_IMG = db.Characters.Find(character.Character_ID).Avatar_IMG;
                }

                db.Entry(character).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Player_ID = new SelectList(db.Players, "Player_ID", "Name", character.Player_ID);
            return View(character);
        }

        //
        // GET: /Character/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Character character = db.Characters.Find(id);
            if (character == null)
            {
                return HttpNotFound();
            }
            return View(character);
        }

        //
        // POST: /Character/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Character character = db.Characters.Find(id);
            db.Characters.Remove(character);
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