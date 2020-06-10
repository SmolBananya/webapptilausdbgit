using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppTilausDB.Models;
using System.Net;
using System.Data.Entity;

namespace WebAppTilausDB.Controllers
{
    public class TilauksetController : Controller
    {
        TilausDBEntities1 db = new TilausDBEntities1();

        // GET: Tilaukset
        public ActionResult Index()
        {
            if (Session["Kayttajatunnus"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {
                List<Tilaukset> model = db.Tilaukset.ToList();
                db.Dispose();

                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Tilaukset tilaukset = db.Tilaukset.Find(id);
            if (tilaukset == null) return HttpNotFound();
            return View(tilaukset);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TilausID,AsiakasID,Toimitusosoite,Postinumero,Tilauspvm,Toimituspvm")] Tilaukset tilaukset
     )
        {
            if (ModelState.IsValid)
            {
                db.Entry(tilaukset).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tilaukset);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TilausID,AsiakasID,Toimitusosoite,Postinumero,Tilauspvm,Toimituspvm")] Tilaukset tilaukset)
        {
            if (ModelState.IsValid)
            {
                db.Tilaukset.Add(tilaukset);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tilaukset);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Tilaukset tilaukset = db.Tilaukset.Find(id);
            if (tilaukset == null) return HttpNotFound();
            return View(tilaukset);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tilaukset tilaukset = db.Tilaukset.Find(id);
            db.Tilaukset.Remove(tilaukset);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



    }
}