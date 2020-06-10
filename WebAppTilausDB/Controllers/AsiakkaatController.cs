using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppTilausDB.Models;


namespace WebAppTilausDB.Controllers
{
    public class AsiakkaatController : Controller
    {
        TilausDBEntities1 db = new TilausDBEntities1();

        // GET: Asiakkaat
        public ActionResult Index()
        {
            if (Session["Kayttajatunnus"] == null)
            {
                return RedirectToAction("login", "home");
            }
            else
            {
                //List<Asiakkaat> model = db.Asiakkaat.ToList();
                var asiakkaat = db.Asiakkaat.Include(a => a.Postitoimipaikat);
                //db.Dispose();

                return View(asiakkaat.ToList());
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Asiakkaat asiakkaat = db.Asiakkaat.Find(id);
            if (asiakkaat == null) return HttpNotFound();
            ViewBag.Postinumero = new SelectList(db.Postitoimipaikat, "PostiID","Postinumero", asiakkaat.PostiID);
            return View(asiakkaat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AsiakasID,Nimi,Osoite,PostiID,Postinumero")] Asiakkaat asiakkaat
    )
        {
            if (ModelState.IsValid)
            {
                db.Entry(asiakkaat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Postinumero = new SelectList(db.Postitoimipaikat, "PostiID","Postinumero", asiakkaat.PostiID);
            return View(asiakkaat);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TuoteID,Nimi,Ahinta,Kuva")] Asiakkaat asiakkaat)
        {
            if (ModelState.IsValid)
            {
                db.Asiakkaat.Add(asiakkaat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(asiakkaat);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Asiakkaat asiakkaat = db.Asiakkaat.Find(id);
            if (asiakkaat == null) return HttpNotFound();
            return View(asiakkaat);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Asiakkaat asiakkaat = db.Asiakkaat.Find(id);
            db.Asiakkaat.Remove(asiakkaat);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}