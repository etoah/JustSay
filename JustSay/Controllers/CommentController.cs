using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Justsay.Models;
using JustSay.Business;

namespace JustSay.Controllers
{
    public class CommentController : Controller
    {
        private JustSayEntities db = new JustSayEntities();

        public ICommentBusiness iComment = new CommentBusiness(new JustSayEntities());

        //
        // GET: /Comment/
        [Authorize(Roles = ("Owner,AdminL,SuperAdmin"))]
        public ViewResult Index(int id = 1, int pageindex = 1)
        {
            return View(iComment.LoadPageEntities(20, pageindex, c => c.Time));
        }
        [Authorize]
        public ActionResult MyComment(int id = 1)
        {
            int memberID = Convert.ToInt32(User.Identity.Name);
            return View(iComment.LoadPageEntities(20, id, c => c.MemberID == memberID, c => c.Time));
        }
        [Authorize]
        public ActionResult FunnyComment(int id, int pageindex = 1)
        {
            ViewBag.FunnyID = id;
            List<Comment> comments=new List<Comment>();
            if (pageindex == 1)
            {
                comments.AddRange(iComment.LoadEntities(c => c.FunnyID == id).OrderByDescending(c => c.Up).Take(5).ToList());
            }
            comments.AddRange(iComment.LoadPageEntities(10, pageindex, c => c.FunnyID == id, c => c.Time));
            return PartialView(comments);
        }

        //
        // GET: /Comment/Details/5
        public ViewResult Details(int id)
        {
            return View(iComment.GetDetail(id));
        }

        //
        // GET: /Comment/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }
        [Authorize]
        public string Up(int id)
        {
            int count = iComment.Up(id);
            if (count > 0)
            {
                return count.ToString();

            }
            else
            {
                return "顶过";
            }

        }

        //
        // POST: /Comment/Create

        [HttpPost]
        [Authorize]
        public ActionResult Create([Bind(Include = "FunnyID,Content")]Comment comment)
        {
            if (ModelState.IsValid)
            {
                // comment.MemberID = Convert.ToInt32(User.Identity.Name);
                iComment.Add(comment);
                return RedirectToAction("Details", "Funny", new { id = comment.FunnyID, pageindex = "1" });
            }
            return View(comment);
        }

        //
        // GET: /Comment/Edit/5

        public ActionResult Edit(int id)
        {
            if (iComment.GetDetail(id).MemberID.ToString() == User.Identity.Name)
            {
                return View(iComment.GetDetail(id));
            }
            return View("~/Views/Common/Denied.cshtml");
        }

        //
        // POST: /Comment/Edit/5

        [HttpPost, Authorize]
        public ActionResult Edit(Comment comment)
        {

            if (comment.MemberID.ToString() == User.Identity.Name)
            {

                if (ModelState.IsValid)
                {
                    iComment.Update(comment);
                    return RedirectToAction("Index");
                }
                return View(comment);
            }
            return View("~/Views/Common/Denied.cshtml");
        }

        //
        // GET: /Comment/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            if (User.IsInRole("AdminL")||User.IsInRole("SuperAdmin")||User.IsInRole("Owner") || iComment.GetDetail(id).MemberID.ToString() == User.Identity.Name)
            {
                return View(iComment.GetDetail(id));
            }
            return View("~/Views/Common/Denied.cshtml");
        }

        //
        // POST: /Comment/Delete/5

        [HttpPost, ActionName("Delete"), Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            if (User.IsInRole("AdminL")||User.IsInRole("SuperAdmin")||User.IsInRole("Owner") || iComment.GetDetail(id).MemberID.ToString() == User.Identity.Name)
            {
                iComment.Delete(id);
                return RedirectToAction("Index");
            }
            return View("~/Views/Common/Denied.cshtml");
        }

      
        protected override void Dispose(bool disposing)
        {
            iComment.Dispose();
            base.Dispose(disposing);
        }
    }
}