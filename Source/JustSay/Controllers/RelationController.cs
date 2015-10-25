using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Justsay.Models;
using JustSay.Business;
using Webdiyer.WebControls.Mvc;

namespace JustSay.Controllers
{
    public class RelationController : Controller
    {
        IRelationBusiness iRelation = new RelationBusiness(new JustSayEntities());
        //
        // GET: /Relation/
        [OutputCache(CacheProfile = "Cache10Sec", VaryByParam = "id")]
        public ViewResult Index(int id = 1)
        {
            return View(iRelation.LoadPageEntities(20, id, r => r.Time,
                n => new Relation { ID = n.ID, ShowName = n.ShowName, ToName = n.ToName, Time=n.Time}));
        }
       // [OutputCache(CacheProfile = "Cache10Sec")]
        public ViewResult Hot()
        {
            return View(iRelation.LoadPageEntities(10, 1, r => r.Time,
                n => new Relation { ID = n.ID, ShowName = n.ShowName, ToName = n.ToName, Time = n.Time }));
        }
        public ViewResult MyRelation(int id = 1)
        {
            return View(iRelation.LoadPageEntities(20, id,r=>r.MemberID.ToString()==User.Identity.Name, r => r.Time));
        }
        public ViewResult Manage(int id = 1)
        {
            return View(iRelation.LoadPageEntities(20, id, r => r.Time,
               n => new Relation { ID = n.ID, ShowName = n.ShowName, ToName = n.ToName, Time = n.Time }));
        }
        [Authorize]
        public ActionResult GetFans(int id)
        {
            string name;
            using (IMemberBusiness iMember = new MemberBusiness(new JustSayEntities()))
            {
                name = iMember.GetDetail(id).RealName;
            }
            return PartialView(iRelation.GetFans(name));
        }

        //
        // GET: /Relation/Details/5
        public ActionResult Details(int id)
        {

            return View(iRelation.GetDetail(id));

        }

        //
        // GET: /Relation/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Relation/Create

        [HttpPost, Authorize]
        public ActionResult Create([Bind(Exclude = "Time,ShowName,MemberID,ID")]Relation relation)
        {
            if (ModelState.IsValid)
            {
                iRelation.Add(relation);
                if (iRelation.IsLoveEachOther(relation.FromName, relation.ToName))
                {
                    ViewBag.IsEmailSuccess= Inform.Email(relation);
                    ViewBag.IsSMSSuccess=Inform.SMS(relation);
                    return View("LoveEachOther",relation);
                }
                return RedirectToAction("Index");
            }
            return View(relation);
        }

        //
        // GET: /Relation/Edit/5

        public ActionResult Edit(int id)
        {

            if (iRelation.GetDetail(id).MemberID.ToString() == User.Identity.Name)
            {
                return View(iRelation.GetDetail(id));
            }
            return View("~/Views/Common/Denied.cshtml");
        }

        //
        // POST: /Relation/Edit/5

        [HttpPost]
        public ActionResult Edit(Relation relation)
        {
            if (relation.MemberID.ToString() == User.Identity.Name)
            {
                if (ModelState.IsValid)
                {
                    iRelation.Update(relation);
                    return RedirectToAction("Index");
                }
                return View(relation);
            }
            return View("~/Views/Common/Denied.cshtml");
        }

        //
        // GET: /Relation/Delete/5

        public ActionResult Delete(int id)
        {
            if (User.IsInRole("AdminL") || User.IsInRole("SuperAdmin") || User.IsInRole("Owner") || iRelation.GetDetail(id).MemberID.ToString() == User.Identity.Name)
            {
                return View(iRelation.GetDetail(id));
            }
            return View("~/Views/Common/Denied.cshtml");

        }

        //
        // POST: /Relation/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            if (User.IsInRole("AdminL") || User.IsInRole("SuperAdmin") || User.IsInRole("Owner") || iRelation.GetDetail(id).MemberID.ToString() == User.Identity.Name)
            {
                iRelation.Delete(id);
                return RedirectToAction("Index");
            }
            return View("~/Views/Common/Denied.cshtml");
        }

        /// <summary>
        /// 搜索   姓名
        /// </summary>
        /// <param name="id">姓名</param>
        /// <param name="pageindex"></param>
        /// <returns></returns>
        public ViewResult Search(string id, int pageindex = 1)
        {
            ViewBag.Key = id;
            if (string.IsNullOrWhiteSpace(id))
                RedirectToAction("Index");
            IPagedList<Relation> list = iRelation.LoadEntities(n => n.ToName.IndexOf(id) != -1).OrderByDescending(n => n.Time).ToPagedList(pageindex, 20);
            return View(list);
        }
        protected override void Dispose(bool disposing)
        {
            iRelation.Dispose();
            base.Dispose(disposing);
        }
    }
}