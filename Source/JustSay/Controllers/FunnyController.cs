using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Justsay.Models;
using JustSay.Business;
using JustSay.Common.DotNetWeb;
using JustSay.Config;
using Webdiyer.WebControls.Mvc;
using JustSay.Common.Extension;


namespace JustSay.Controllers
{
    public class FunnyController : Controller
    {
        IFunnyBusiness iFunny = new FunnyBusiness(new JustSayEntities());

        //
        // GET: /Funny/
        [OutputCache(CacheProfile = "Cache10Sec", VaryByParam = "id")]
        public ViewResult Index(int id = 1)
        {
            //
            ViewBag.Title = "热门点滴";
            return View("Index", iFunny.LoadPageEntities(20, id, f => f.Time.AddDays(HotConfig.DefaultFunnyHomeHotByDay) > DateTime.Now&&f.Status==3, f => f.Up));
        }
         [Authorize(Roles = ("Owner,SuperAdmin,AdminL"))]
        public ViewResult Manage(int id = 1)
        {
            //
            return View("Manage", iFunny.LoadPageEntities(20, id, f => f.Time));
        }

        public ViewResult HotByHour( int id = 24,int pageindex = 1)
        {
            ViewBag.Title = id + "小时 最热点滴";
            ViewBag.Para= id;
            return View("HotByPara", iFunny.LoadPageEntities(20, pageindex, f => f.Time.AddHours(id) > DateTime.Now && f.Status == 3, f => f.Up));
        }
        //[OutputCache(CacheProfile = "Cache10Sec", VaryByParam = "id")]
        public ViewResult Hot(int id = 7)
        {
            return View("Hot", iFunny.LoadPageEntities(20, 1, f => f.Time.AddDays(id) > DateTime.Now && f.Status == 3, f => f.Up));
        }
        [OutputCache(CacheProfile = "Cache10Sec", VaryByParam = "id,pageindex")]
        public ViewResult HotByDay( int id = 7,int pageindex = 1)
        {
            ViewBag.Title = id + "天 最热点滴";
            ViewData["para"] = id;
            return View("HotByPara", iFunny.LoadPageEntities(20, pageindex, f => f.Time.AddDays(id) > DateTime.Now && f.Status == 3, f => f.Up));
        }
        [OutputCache(CacheProfile = "Cache10Sec", VaryByParam = "id")]
        public ViewResult New(int id = 1)
        {
            ViewBag.Title = "最新点滴";
            return View("Index", iFunny.LoadPageEntities(20, id, f => f.Status == 3, f => f.Time));
        }

        [Authorize]
        public string Up(int id)
        {
            int count = iFunny.Up(id);
            if (count>0)
            {
                return ("<span class='glyphicon glyphicon-thumbs-up'></span>" + count.ToString());              
            }
            else
            {
                return "顶过";
            }
           
        }

        //
        // GET: /Funny/Details/5
        [OutputCache(CacheProfile = "Cache10Sec", VaryByParam = "id,pageindex")]
        public ActionResult Details(int id, int pageindex = 1)
        {
            List<Comment> list;
            List<Comment> hotlist = new List<Comment>();
            using (ICommentBusiness iComment = new CommentBusiness(new JustSayEntities()))
            {
                list=(iComment.LoadPageEntities(10, pageindex, c => c.FunnyID == id, c => c.Time));

                if (Request.IsAjaxRequest())
                {
                    ViewBag.FunnyID = id;
                    return PartialView("~/Views/Comment/FunnyComment.cshtml", list);

                }
                if (pageindex == 1 && iComment.Count(c => c.FunnyID == id) > 20)
                {
                    hotlist=iComment.LoadEntities(c => c.FunnyID == id).OrderByDescending(c => c.Up).Take(5).ToList();
                    ViewData["IsHasHot"] = true;
                }
                Funny funny = iFunny.GetDetail(id);
                ViewData["list"] = list;
                ViewData["hotlist"] = hotlist;
               
                return View(funny);
            }
            
        }

        public ActionResult MyFunny(int id=1)
        {
            int MemberID=Convert.ToInt32(User.Identity.Name);
            return View(iFunny.LoadPageEntities(20, id, f => f.MemberID == MemberID, f => f.Time));
        }


        //
        // GET: /Funny/Create
        [Authorize]
        [OutputCache(CacheProfile = "Cache10Sec")]
        public ActionResult Create()
        {
            IConfessBusiness iConfess = new ConfessBusiness(new JustSayEntities());
            List<SelectListItem> selectList = 
                iConfess.LoadEntities(c=>c.MemberID.ToString()==User.Identity.Name).
                Select(c => new SelectListItem { 
                               Text=c.Content.SubstringLength(10),
                               Value=c.ID.ToString()
                               }).ToList();
            ViewBag.List = selectList;
            return View();
        }

        //
        // POST: /Funny/Create

        [HttpPost]
        [ValidateInput(false)]
        [ValidateMvcCaptcha]
        public ActionResult Create([Bind(Include = "ShowName,Title,Content,ConfessID ,ImgUrl ")]Funny funny)
        {
            IMemberBusiness iMember=new MemberBusiness(new JustSayEntities());
            if (iMember.IsLimitPost(User.Identity.Name.ToInt()))
            {
                ViewData["Validate"] = true;               
            }
            else
            {
                ModelState.Remove("_mvcCaptchaText");
            }
            if (ModelState.IsValid)
            {

                iFunny.Add(funny);
                return RedirectToAction("New");
            }
            return View(funny);
        }

        //
        // GET: /Funny/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            if (iFunny.GetDetail(id).MemberID.ToString() == User.Identity.Name)
            {
                return View(iFunny.GetDetail(id));
            }
            return View("~/Views/Common/Denied.cshtml");
        }

        //
        // POST: /Funny/Edit/5

        [HttpPost,Authorize]
        public ActionResult Edit([Bind(Include = "ID,Title,Content,ConfessID ,ImgUrl")]Funny funny)
        {
            Funny ofunny = iFunny.GetDetail(funny.ID); //= iFunny.LoadEntities().AsNoTracking().First(n => n.ID == funny.ID);
            if (ofunny.MemberID.ToString() == User.Identity.Name)
            {
                if (ModelState.IsValid)
                {
                    ofunny.Title = funny.Title;
                    ofunny.Content=funny.Content;
                    ofunny.ConfessID = funny.ConfessID;
                    ofunny.ImgUrl = funny.ImgUrl;
                    iFunny.Update(ofunny);
                    return RedirectToAction("Index");
                }
                return View(funny);
            }
            return View("~/Views/Common/Denied.cshtml");
        }

        //
        // GET: /Funny/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            if (User.IsInRole("AdminL")||User.IsInRole("SuperAdmin")||User.IsInRole("Owner") || iFunny.GetDetail(id).MemberID.ToString() == User.Identity.Name)
            {
                return View(iFunny.GetDetail(id));
            }
            return View("~/Views/Common/Denied.cshtml");
        }

        //
        // POST: /Funny/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            if (User.IsInRole("AdminL")||User.IsInRole("SuperAdmin")||User.IsInRole("Owner") || iFunny.GetDetail(id).MemberID.ToString() == User.Identity.Name)
            {
                iFunny.Delete(id);
                return RedirectToAction("Index");
            }
            return View("~/Views/Common/Denied.cshtml");
        }
        [Authorize(Roles = ("Owner,AdminL,SuperAdmin,Senior,User"))]
        public ActionResult Review(int id, int pageindex)//pageindex为是否通过（1）
        {
            if (pageindex == 1)
            {
                iFunny.Review(id, true);
               
            }
            else
            {
                iFunny.Review(id, false);
            }
            return RedirectToAction("ReviewDetail");
        }

        [HttpGet]
        [Authorize(Roles = ("Owner,AdminL,SuperAdmin,Senior,User"))]
        public ActionResult ReviewDetail()
        {
            var query = iFunny.LoadEntities(n => n.Status == 0&&n.MemberID!=Convert.ToInt32(User.Identity.Name));
            int count = query.Count();
            if (count>2)
            {
                Random random = new Random();
                Funny funny = query.OrderByDescending(f => f.Time).Skip(random.Next(count)).Take(1).ToList()[0];
                return View(funny);
            }
            return View();
            
        }



        protected override void Dispose(bool disposing)
        {
            iFunny.Dispose();
            base.Dispose(disposing);
        }
    }
}