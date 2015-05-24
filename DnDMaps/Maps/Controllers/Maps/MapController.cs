using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maps.Models;
using AutoMapper;
using System.IO;
using System.Configuration;

namespace Maps.Controllers
{
    public class MapController : Controller
    {
        private Entities db = new Entities(); //database context class, should be cleaned up on de-struct

        //
        // GET: /Map/
        // "Root" / Listing Page for any maps..
        public ActionResult Index()
        {
            //try
            //{
                Mapper.CreateMap<Map, vmMap>(); //auto map entity model to view model

                var model = db.Maps.ToList();

                return View(Mapper.Map(model, new List<vmMap>()));
            //}
            //catch (Exception)
            //{
                //return View();
            //}
        }

        //PLAY the Map :)
        [HttpGet]
        public ActionResult Play(int id)
        {
            ViewBag.Chars = db.Characters.ToList();

            return View(db.Maps.Find(id));
        }

        #region CRUD
      

        //
        // GET: /Map/Create

        public ActionResult Create()
        {
            vmMap model = new vmMap();

            //setup default properties for a new viewmodel
            model.Active = true;
            model.SortOrder = 1;

            return View(model);
        }

        //
        // POST: /Map/Create

        [HttpPost]
        public ActionResult Create(vmMap map)
        {
            if (ModelState.IsValid)
            {

                Mapper.CreateMap<vmMap,Map>(); //auto map view model to a fresh entity model...
                Map newMap = new Map();
                Mapper.Map(map, newMap);


                if (map.Background_IMG_Upload != null && map.Background_IMG_Upload.ContentLength > 0)
                {

                    //generate a guid, store the file upload as this name...
                    Guid fileName = Guid.NewGuid();

                    string targetFolder = HttpContext.Server.MapPath(ConfigurationManager.AppSettings["UPLOADED_MAP_IMAGES_RELATIVE_PATH"]);
                    string targetPath = Path.Combine(targetFolder, fileName.ToString());
                    targetPath += "." + map.Background_IMG_Upload.FileName.Split('.')[1];

                    map.Background_IMG_Upload.SaveAs(targetPath); //save the uploaded file to the server's file system...

                    newMap.Background_IMG_Path = fileName + "." + map.Background_IMG_Upload.FileName.Split('.')[1]; //store the path of uploaded file in the database entity..

                    db.Maps.Add(newMap);
                    db.SaveChanges();

                }

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

            Mapper.CreateMap<Map, vmMap>(); //auto map entity model to view model
            vmMap model = new vmMap();


            return View(Mapper.Map(map, model));

        
        }

        //
        // POST: /Map/Edit/5

        [HttpPost]
        public ActionResult Edit(vmMap model)
        {
            if (ModelState.IsValid)
            {

                Mapper.CreateMap<vmMap, Map>(); //auto map view model to a fresh entity model...
                Map map = db.Maps.Find(model.Map_ID);
                string current_filePath = map.Background_IMG_Path;
                Mapper.Map(model, map);

                if (model.Background_IMG_Upload != null && model.Background_IMG_Upload.ContentLength > 0)
                {

                    //generate a guid, store the file upload as this name...
                    Guid fileName = Guid.NewGuid();

                    string targetFolder = HttpContext.Server.MapPath(ConfigurationManager.AppSettings["UPLOADED_MAP_IMAGES_RELATIVE_PATH"]);
                    string targetPath = Path.Combine(targetFolder, fileName.ToString());
                    targetPath += "." + model.Background_IMG_Upload.FileName.Split('.')[1];

                    model.Background_IMG_Upload.SaveAs(targetPath); //save the uploaded file to the server's file system...

                    map.Background_IMG_Path = fileName + "." + model.Background_IMG_Upload.FileName.Split('.')[1]; //store the path of uploaded file in the database entity..

                }
                else
                    map.Background_IMG_Path = current_filePath;


                db.Entry(map).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            return View(model);
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
        #endregion

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}