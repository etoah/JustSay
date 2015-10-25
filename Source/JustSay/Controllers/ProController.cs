using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Justsay.Models;

namespace JustSay.Controllers
{
     [Authorize(Roles = ("Owner,SuperAdmin"))]
    public class ProController : Controller
    {
        private JustSayEntities db = new JustSayEntities();

        //
        // GET: /Pro/

        public ViewResult Index()
        {
            return View(db.Pros.ToList());
        }

        //
        // GET: /Pro/Details/5

        public ViewResult Details(int id)
        {
            Pro pro = db.Pros.Find(id);
            return View(pro);
        }

        //
        // GET: /Pro/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Pro/Create

        [HttpPost]
        public ActionResult Create(Pro pro)
        {
            if (ModelState.IsValid)
            {
                db.Pros.Add(pro);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(pro);
        }
        
        //
        // GET: /Pro/Edit/5
 
        public ActionResult Edit(int id)
        {
            Pro pro = db.Pros.Find(id);
            return View(pro);
        }

        //
        // POST: /Pro/Edit/5

        [HttpPost]
        public ActionResult Edit(Pro pro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pro);
        }

        //
        // GET: /Pro/Delete/5
 
        public ActionResult Delete(int id)
        {
            Pro pro = db.Pros.Find(id);
            return View(pro);
        }

        //
        // POST: /Pro/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Pro pro = db.Pros.Find(id);
            db.Pros.Remove(pro);
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