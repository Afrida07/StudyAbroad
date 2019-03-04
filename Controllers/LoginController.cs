using StudyAbroad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudyAbroad.Controllers
{
    public class LoginController : Controller
    {
        private StudyAbroadEntities db = new StudyAbroadEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Autherize(StudyAbroad.Models.Userdetail userModel)
        {
            using (StudyAbroadEntities db = new StudyAbroadEntities())
            {
                var userDetails = db.Userdetails.Where(x => x.UserName == userModel.UserName && x.Password == userModel.Password).FirstOrDefault();
                if (userDetails == null)
                {
                    userModel.LoginErrorMessage = "Wrong Username or Password";
                    return View("Index", userModel);
                }

                else
                {
                    Session["UserID"] = userDetails.UserID;
                    Session["UserName"] = userDetails.UserName;
                    return RedirectToAction("Loggedin", "Home");
                }



            }
        }


              //logout function


            public ActionResult Logout()
        {

            int userID = (int)Session["UserID"];
            Session.Abandon();

             return RedirectToAction("Index", "Home");
        }


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "UserID,Name,UserName,Email,Password")] Userdetail userdetail)
        {
            if (ModelState.IsValid)
            {
                db.Userdetails.Add(userdetail);
                db.SaveChanges();
                return RedirectToAction("Login");
            }

            return View(userdetail);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,Name,UserName,Email,Password")] Userdetail userdetail)
        {
            if (ModelState.IsValid)
            {
                db.Userdetails.Add(userdetail);
                db.SaveChanges();
                return RedirectToAction("Login");
            }

            return View(userdetail);
        }


    }
}