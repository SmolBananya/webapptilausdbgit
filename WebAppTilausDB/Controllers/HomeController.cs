﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppTilausDB.Models;

namespace WebAppTilausDB.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["Kayttajatunnus"] == null)
            {
                ViewBag.LoggedStatus = "Out";
            }
            else ViewBag.LoggedStatus = "In";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Tietoa Yrityksestä";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Yrityksen yhteystiedot";

            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(Tunnistus LoginModel)
        {
            TilausDBEntities1 db = new TilausDBEntities1();
            //Haetaan käyttäjän/Loginin tiedot annetuilla tunnustiedoilla tietokannasta LINQ -kyselyllä
            var LoggedUser = db.Tunnistus.SingleOrDefault(x => x.Kayttajatunnus == LoginModel.Kayttajatunnus && x.Salasana == LoginModel.Salasana);
            if (LoggedUser != null)
            {
                ViewBag.LoginMessage = "Onnistunut sisäänkirjatuminen";
                ViewBag.LoggedStatus = "Sisään kirjautunut";
                Session["Kayttajatunnus"] = LoggedUser.Kayttajatunnus;
                return RedirectToAction("Index", "Home"); //Tässä määritellään mihin onnistunut kirjautuminen johtaa --> Home/Index
            }
            else
            {
                ViewBag.LoginMessage = "Onnistunut uloskirjautuminen";
                ViewBag.LoggedStatus = "Ulos kirjautunut";
                LoginModel.LoginErrorMessage = "Tuntematon käyttäjätunnus tai salasana.";
                return View("Login", LoginModel);
            }
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            ViewBag.LoggedStatus = "Out";
            return RedirectToAction("Index", "Home"); //Uloskirjautumisen jälkeen pääsivulle
        }


    }
}