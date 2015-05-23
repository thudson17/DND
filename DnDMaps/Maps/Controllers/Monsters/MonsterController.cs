using AutoMapper;
using Maps.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Maps.Controllers
{
    public class MonsterController : Controller
    {
        private Entities db = new Entities(); //database context class, should be cleaned up on de-struct

        //Root Monster Page, Show existing list of all monsters
        public ActionResult Index()
        {
            Mapper.CreateMap<Monster, vmMonster>(); //auto map entity model to view model

            var model = db.Monsters.ToList();

            return View(Mapper.Map(model, new List<vmMonster>()));
        }

        //Create a new monster
        public ActionResult Create()
        {
            vmMonster model = new vmMonster();

            return View(model);
        }

        //
        // POST: /Monster/Create
        [HttpPost]
        public ActionResult Create(vmMonster monster)
        {
            if (ModelState.IsValid)
            {

                Mapper.CreateMap<vmMonster, Monster>(); //auto map view model to a fresh entity model...
                Monster newMonster = new Monster();
                Mapper.Map(monster, newMonster);


                if (monster.Avatar_File != null && monster.Avatar_File.ContentLength > 0)
                {
                    //generate a guid, store the file upload as this name...
                    Guid fileName = Guid.NewGuid();

                    string targetFolder = HttpContext.Server.MapPath(ConfigurationManager.AppSettings["UPLOADED_MONSTER_IMAGES_RELATIVE_PATH"]);
                    string targetPath = Path.Combine(targetFolder, fileName.ToString());
                    targetPath += "." + monster.Avatar_File.FileName.Split('.')[1];

                    monster.Avatar_File.SaveAs(targetPath); //save the uploaded file to the server's file system...

                    newMonster.Avatar_IMG = fileName + "." + monster.Avatar_File.FileName.Split('.')[1]; //store the path of uploaded file in the database entity..

                    db.Monsters.Add(newMonster);
                    db.SaveChanges();

                }

                return RedirectToAction("Index");
            }

            return View(monster);
        }

        //
        // GET: /Map/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Monster map = db.Monsters.Find(id);
            if (map == null)
            {
                return HttpNotFound();
            }

            Mapper.CreateMap<Monster, vmMonster>(); //auto map entity model to view model
            vmMonster model = new vmMonster();
            return View(Mapper.Map(map, model));


        }

        //
        // POST: /Map/Edit/5

        [HttpPost]
        public ActionResult Edit(vmMonster model)
        {
            if (ModelState.IsValid)
            {

                Mapper.CreateMap<vmMonster, Monster>(); //auto map view model to a fresh entity model...
                Monster monster = db.Monsters.Find(model.Monster_ID);
                string current_filePath = monster.Avatar_IMG;
                Mapper.Map(model, monster);

                if (model.Avatar_File != null && model.Avatar_File.ContentLength > 0)
                {

                    //generate a guid, store the file upload as this name...
                    Guid fileName = Guid.NewGuid();

                    string targetFolder = HttpContext.Server.MapPath(ConfigurationManager.AppSettings["UPLOADED_MONSTER_IMAGES_RELATIVE_PATH"]);
                    string targetPath = Path.Combine(targetFolder, fileName.ToString());
                    targetPath += "." + model.Avatar_File.FileName.Split('.')[1];

                    model.Avatar_File.SaveAs(targetPath); //save the uploaded file to the server's file system...

                    monster.Avatar_IMG = fileName + "." + model.Avatar_File.FileName.Split('.')[1]; //store the path of uploaded file in the database entity..

                }
                else
                {
                    monster.Avatar_IMG = current_filePath;
                }


                db.Entry(monster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            return View(model);
        }

        //
        // GET: /Monster/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Monster monster = db.Monsters.Find(id);
            if (monster == null)
            {
                return HttpNotFound();
            }
            return View(monster);
        }

        //
        // POST: /Monster/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Monster monster = db.Monsters.Find(id);
            db.Monsters.Remove(monster);
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
