using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Justsay.Models;
using JustSay.Business;
using JustSay.Common.Extension;
using Webdiyer.WebControls.Mvc;
using JustSay.Config;
namespace JustSay.Controllers
{ 
    public class MessageController : Controller
    {
        IMessageBusiness iMessage = new MessageBusiness(new JustSayEntities());

        //
        // GET: /Message/
        [Authorize(Roles = ("Owner,AdminL,SuperAdmin"))]
        public ViewResult Index(int id=1)
        {
            return View(iMessage.LoadPageEntities(20,id,m=>m.IsNew));
        }

        //
        // GET: /Message/Details/5
        
        public ViewResult Details(int id=1)
        {


            return View(iMessage.GetDetail(id));
        }
        [Authorize]
        [HttpGet]
        public string Reading(int id)
        {
            if (iMessage.GetDetail(id).ToID.ToString() == User.Identity.Name)
            {
                iMessage.Reading(id);
                return "";
            }
            return "没有权限";
        }

        //
        // GET: /Message/Create
        [Authorize]
        [HttpGet]
        public ActionResult Create(string idstr,string name)
        {
            ViewData["ID"] = idstr;
            ViewData["name"] =HttpUtility.UrlDecode(name) ;
            return View();
        }


        [Authorize]
        public ActionResult MyNewMessage(int id=1)
        {
            return View(iMessage.LoadPageEntities(5, id,m=>m.ToID.ToString()==User.Identity.Name, m => m.Time));
        }

        [Authorize]
        public ActionResult MyMessage(int id = 1)
        {
            return View(iMessage.LoadPageEntities(5, id, m => m.FromID.ToString() == User.Identity.Name, m => m.Time));
        }

        //
        // POST: /Message/Create

        [HttpPost, Authorize,ValidateMvcCaptcha]
        public ActionResult Create(string ToID,string content,string toName)
        {
            IMemberBusiness iMember = new MemberBusiness(new JustSayEntities());
            if ( HotConfig.IsControlMessage == 1&&iMember.IsLimitPost(User.Identity.Name.ToInt()))
            {
                ViewData["Validate"] = true;

            }
            else
            {
                ModelState.Remove("_mvcCaptchaText");
            }
            if (ModelState.IsValid)
            {
                Message message = new Message();
                Member member = MemberBusiness.GetUserInfo();
                message.ToID = ToID.UidDescrypt();
                message.FromID = member.ID;
                message.FromName = member.ShowName;
                message.Content = content;
                message.ToName = toName;
                iMessage.Add(message);
                return Content("发送成功");  
            }
            return Content("发送频繁60s以后再来");
        }
        


        //
        // GET: /Message/Delete/5
        
        public ActionResult Delete(int id)
        {
            if (User.IsInRole("AdminL")||User.IsInRole("SuperAdmin")||User.IsInRole("Owner") || iMessage.GetDetail(id).FromID.ToString() == User.Identity.Name)
            {
                return View(iMessage.GetDetail(id));
            }
            return View("~/Views/Common/Denied.cshtml");

         
        }

        //
        // POST: /Message/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            if (User.IsInRole("AdminL")||User.IsInRole("SuperAdmin")||User.IsInRole("Owner") || iMessage.GetDetail(id).FromID.ToString() == User.Identity.Name)
            {
                iMessage.Delete(id);
                return RedirectToAction("Index");
            }
            return View("~/Views/Common/Denied.cshtml");

        }

        [Authorize]
        public string Count()
        {
            int id = Convert.ToInt32(User.Identity.Name);
            return iMessage.Count(m=>m.ToID==id&&m.IsNew==true).ToString();

        }

        protected override void Dispose(bool disposing)
        {
            iMessage.Dispose();
            base.Dispose(disposing);
        }
    }
}