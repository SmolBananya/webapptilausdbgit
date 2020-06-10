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
    public class TunnistusController : Controller
    {
        TilausDBEntities1 db = new TilausDBEntities1();

        // GET: Tunnistus
        public ActionResult Index()
        {
            if (Session["Kayttajatunnus"] == null)
            {
                ViewBag.LoggedStatus = "Out";
                return RedirectToAction("login", "home");
            }
            else
            {
                ViewBag.LoggedStatus = "In";
                List<Tunnistus> model = db.Tunnistus.ToList();
                db.Dispose();

                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (Session["Kayttajatunnus"] == null)
            {
                ViewBag.LoggedStatus = "Out";
                return RedirectToAction("login", "home");
            }
            else
            {
                ViewBag.LoggedStatus = "In";
                if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                Tunnistus tunnistus = db.Tunnistus.Find(id);
                if (tunnistus == null) return HttpNotFound();
                return View(tunnistus);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LogInID,Kayttajatunnus,Salasana")] Tunnistus tunnistus
     )
        {
            if (ModelState.IsValid)
            {
                db.Entry(tunnistus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tunnistus);
        }

        public ActionResult Create()
        {
            if (Session["Kayttajatunnus"] == null)
            {
                ViewBag.LoggedStatus = "Out";
                return RedirectToAction("login", "home");
            }
            else
            {
                ViewBag.LoggedStatus = "In";
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LogInID,Kayttajatunnus,Salasana")] Tunnistus tunnistus)
        {
            if (ModelState.IsValid)
            {
                db.Tunnistus.Add(tunnistus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tunnistus);
        }

        public ActionResult Delete(int? id)
        {
            if (Session["Kayttajatunnus"] == null)
            {
                ViewBag.LoggedStatus = "Out";
                return RedirectToAction("login", "home");
            }
            else
            {
                ViewBag.LoggedStatus = "In";
                if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                Tunnistus tunnistus = db.Tunnistus.Find(id);
                if (tunnistus == null) return HttpNotFound();
                return View(tunnistus);
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tunnistus tunnistus = db.Tunnistus.Find(id);
            db.Tunnistus.Remove(tunnistus);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



    }
}