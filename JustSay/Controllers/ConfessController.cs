using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Justsay.Models;
using JustSay.Business;
using JustSay.Config;
using Webdiyer.WebControls.Mvc;
using JustSay.Common.Extension;

namespace JustSay.Controllers
{
    public class ConfessController : Controller
    {
        IConfessBusiness iConfess = new ConfessBusiness(new JustSayEntities());
        //
        // GET: /Confess/
        [OutputCache(CacheProfile = "Cache10Sec", VaryByParam = "id")]
        public ViewResult Index(int id = 1)
        {
            //
            return View("Index", iConfess.LoadPageEntities(20, id, f => f.Time.AddDays(HotConfig.DefaultConfessHomeHotByDay) > DateTime.Now, f => f.Up));
        }

        public ViewResult Manage(int id = 1)
        {
            //
            return View("Manage", iConfess.LoadPageEntities(20, id, f => f.Time));
        }
        [OutputCache(CacheProfile = "Cache10Sec", VaryByParam = "pageindex,hours")]
        public ViewResult HotByHour(int hours = 224, int pageindex = 1)
        {
            ViewBag.Para = hours;
            return View("HotByPara", iConfess.LoadPageEntities(20, pageindex, f => f.Time.AddHours(hours) > DateTime.Now, f => f.Up));
        }
        [OutputCache(CacheProfile = "Cache10Sec", VaryByParam = "pageindex,days")]
        public ViewResult HotByDay(int days = 7, int pageindex = 1)
        {
            ViewData["para"] = days;
            return View("HotByPara", iConfess.LoadPageEntities(20, pageindex, f => f.Time.AddDays(days) > DateTime.Now, f => f.Up));
        }

       // [OutputCache(CacheProfile = "Cache10Sec")]
        public ViewResult Hot()
        {
            return View("Hot", iConfess.LoadEntities(f => f.Time.AddDays(HotConfig.HomeHotConfessDay) > DateTime.Now                  
                ).OrderByDescending(f=>f.Up).Take(4).ToList());
        }
        [OutputCache(CacheProfile = "Cache10Sec", VaryByParam = "id")]
        public ViewResult New(int id = 1)
        {
            return View("Index", iConfess.LoadPageEntities(20, id, f => f.Time));
        }

        public ActionResult MyConfess(int id = 1)
        {
            return View(iConfess.LoadPageEntities(20, id, c => c.MemberID.ToString() == User.Identity.Name, c => c.Time));
        }

        
        public string Up(int id)
        {
            if (User.Identity.IsAuthenticated)
                RedirectToAction("logon","member");
            int count = iConfess.Up(id);
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
        // GET: /Confess/Details/5

        public ViewResult Details(int id)
        {

            return View(iConfess.GetDetail(id));

        }

        //
        // GET: /Confess/Create
        [Authorize]
        public ActionResult Create()
        {

            return View();
        }

        //
        // POST: /Confess/Create

        [HttpPost]
        [Authorize]
        [ValidateInput(false)]
        [ValidateMvcCaptcha]
        public ActionResult Create([Bind(Include = "Content,Message,ToEmail,ToPhone,ToName,ImgUrl,FlashUrl,MusicUrl,ViewName")]Confess confess)
        {
            IMemberBusiness iMember = new MemberBusiness(new JustSayEntities());
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
                iConfess.Add(confess);
             //   IMemberBusiness iMember = new MemberBusiness(new JustSayEntities());
                confess.Member = iMember.GetDetail(Convert.ToInt32(User.Identity.Name));
                Relation relation = new Relation
                {
                    FromEmail = confess.Member.Email,
                    ShowName = confess.Member.ShowName,
                    MemberID = confess.Member.ID,
                    FromPhone = confess.Member.Phone,
                    FromName = confess.Member.RealName,
                    ToEmail = confess.ToEmail,
                    ToName = confess.ToName,
                    ToPhone = confess.ToPhone,

                };

                ViewBag.IsEmailSuccess = Inform.Email(confess);
                ViewBag.IsSMSSuccess = false;
                ViewBag.IsSMSMsg = "";
                if (!string.IsNullOrEmpty(confess.ToPhone))
                {
                    if (confess.Member.Score > 50)
                    {
                        ViewBag.IsSMSSuccess = Inform.SMS(confess);
                        confess.Member.Score -= 50;
                        ViewBag.IsSmsSuccess = true;
                    }
                    else
                    {
                        ViewBag.IsSMSMsg = "节操币不足50，请多发贴，多回贴";
                    }
                }

                ViewData["ConfessID"] = confess.ID;
                return View("~/Views/Relation/Create.cshtml", relation);
            }

            return View(confess);
        }

        //
        // GET: /Confess/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            if (iConfess.GetDetail(id).MemberID.ToString() == User.Identity.Name)
            {
                Confess confess = iConfess.GetDetail(id);
                return View(confess);
            }
            return View("~/Views/Common/Denied.cshtml");

        }

        //
        // POST: /Confess/Edit/5

        [HttpPost]
        public ActionResult Edit([Bind(Include = "ID,Content,Message,ViewName")]Confess confess)
        {
            Confess oconfess = iConfess.GetDetail(confess.ID);
            if (oconfess.MemberID.ToString() == User.Identity.Name)
            {
                ModelState.Remove("ToEmail");
                if (ModelState.IsValid)
                {
                    oconfess.Content = confess.Content;
                    oconfess.Message = confess.Message;
                    oconfess.ViewName = confess.ViewName;
                    iConfess.Update(oconfess);
                    return RedirectToAction("Details", new { id = confess.ID });
                }
                return View(confess);
            }
            return View("~/Views/Common/Denied.cshtml");
        }

        //
        // GET: /Confess/Delete/5

        public ActionResult Delete(int id)
        {
            if (User.IsInRole("AdminL")||User.IsInRole("SuperAdmin")||User.IsInRole("Owner") || iConfess.GetDetail(id).MemberID.ToString() == User.Identity.Name)
            {
                return View(iConfess.GetDetail(id));
            }
            return View("~/Views/Common/Denied.cshtml");

        }

        //
        // POST: /Confess/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            if (User.IsInRole("AdminL")||User.IsInRole("SuperAdmin")||User.IsInRole("Owner") || iConfess.GetDetail(id).MemberID.ToString() == User.Identity.Name)
            {
                iConfess.Delete(id);


                return RedirectToAction("Index");
            }
            return View("~/Views/Common/Denied.cshtml");

        }


        public ViewResult Search(string id, int pageindex = 1)
        {

            ViewBag.Key = id;
            if (string.IsNullOrWhiteSpace(id))
                RedirectToAction("Index");
            IPagedList<Confess> list = iConfess.LoadEntities(n => n.ToName.IndexOf(id) != -1).OrderByDescending(n => n.Time).ToPagedList(pageindex, 20);
            return View(list);
        }

        protected override void Dispose(bool disposing)
        {
            iConfess.Dispose();
            base.Dispose(disposing);
        }
    }
}