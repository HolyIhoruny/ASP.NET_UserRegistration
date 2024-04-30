using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserRegistration.Models;

namespace UserRegistration.Controllers
{
    public class HomeController : Controller
    {
        DatabaseEntities db = new DatabaseEntities();

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(User_Table ut)
        {

            if (db.User_Table.Any(x=>x.UserName == ut.UserName))
            {
                ViewBag.Notification = "This account has already existed";
                return View();
            }

            else
            {
                db.User_Table.Add(ut);
                db.SaveChanges();

                Session["Id"] = ut.Id.ToString();
                Session["UserName"] = ut.UserName.ToString();
                return RedirectToAction("Display");
            }
          
        }

        [HttpGet]
        public ActionResult Display()
        {
            var obj = db.User_Table.ToList();
            return View(obj);
        }
        
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var obj = db.User_Table.Find(id);
            return View(obj);
        }

        [HttpPost]
        public ActionResult Edit(User_Table ut)
        {
            db.Entry(ut).State = System.Data.Entity.EntityState.Modified; 
            db.SaveChanges();

            return RedirectToAction("Display");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var obj = db.User_Table.Find(id);
            return View(obj);
        }

        [HttpPost]
        public ActionResult Delete(User_Table ut)
        {
            db.Entry(ut).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();

            return RedirectToAction("Display");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var obj = db.User_Table.Find(id);
            return View(obj);
        }

        
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("LoginUp", "Home");
        }

        [HttpGet]
        public ActionResult LoginUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginUp(User_Table ut)
        {
            var checkLogin = db.User_Table.Where(x=>x.UserName.Equals(ut.UserName)
            && x.Password.Equals(ut.Password)).FirstOrDefault();

            if (checkLogin != null)
            {

                Session["Id"] = ut.Id.ToString();
                Session["UserName"] = ut.UserName.ToString();
                return RedirectToAction("Display");
            }

            else
            {
                ViewBag.Notification = "Wrong Username or Password";
            }

            return View();
        }


    }
}