using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Justsay.Models;
using JustSay.Business;
using JustSay.Common.DotNetCode;
using JustSay.Common.DotNetEncrypt;

namespace JustSay.Controllers
{
    public class AccountController : Controller
    {
        private JustSayEntities db = new JustSayEntities();
        IMemberBusiness iMember = new MemberBusiness(new JustSayEntities());
        //
        // GET: /Member/


        [Authorize(Roles = ("Owner,SuperAdmin"))]
        public ActionResult Index(int id = 1)
        {
            if (User.IsInRole("SuperAdmin") || User.IsInRole("Owner"))
            {
                return View(iMember.LoadPageEntities(20, id, m => m.RoleID >= (int)RoleNo.AdminL, c => c.Score));
            }
            return View("~/Views/Common/Denied.cshtml");

        }

        //
        // GET: /Member/Details/5
        [Authorize(Roles = ("Owner,SuperAdmin"))]
        public ViewResult Details(int id)
        {
            Member member = iMember.GetDetail(id);
            member.Email = StringHelper.HideEmail(member.Email);
            return View(member);
        }
        [Authorize]
        public ViewResult MySpace()
        {
            int id = Convert.ToInt32(User.Identity.Name);
            Member member = iMember.GetDetail(id);
            member.Email = StringHelper.HideEmail(member.Email);
            return View(member);
        }
        //
        // GET: /Member/Create

        public ActionResult Create()
        {
            // ViewBag.RoleID = new SelectList(db.Roles, "ID", "RoleName");
            return View();
        }

        //
        // POST: /Member/Create

        [HttpPost]
        public ActionResult Create(Member member)
        {
            if (ModelState.IsValid)
            {
                //if (member.Password != member.RepPassword)
                //{
                //    ModelState.AddModelError("RepPassword", "密码不一样");
                //    return View(member);
                //}
                if (iMember.GetDetail(m => m.Email == member.Email) != null)
                {
                    ModelState.AddModelError("Email", "账号已注册");
                    return View(member);
                }
                member.RoleID = 5;
                iMember.Add(member);
                return RedirectToAction("introduce","home");

            }

            //  ViewBag.RoleID = new SelectList(db.Roles, "ID", "RoleName", member.RoleID);
            return View(member);
        }

        //
        // GET: /Member/Edit/5
        [Authorize]
        public ActionResult Edit()
        {
            int id = Convert.ToInt32(User.Identity.Name);
            if (iMember.GetDetail(id).ID.ToString() == User.Identity.Name || User.IsInRole("SuperAdmin") || User.IsInRole("Owner"))
            {

                Member member = iMember.GetDetail(id);
                return View(member);
            }
            return View("~/Views/Common/Denied.cshtml");
        }

        //
        // POST: /Member/Edit/5

        [HttpPost]
        public ActionResult Edit([Bind(Include = "ID,ShowName,QQ,Phone,RealName,RoleID")]Member member)
        {
            if (member.ID.ToString() == User.Identity.Name || User.IsInRole("SuperAdmin") || User.IsInRole("Owner"))
            {
                ModelState.Remove("Email");
                ModelState.Remove("Password");
                if (ModelState.IsValid)
                {
                    Member omember = iMember.GetDetail(member.ID);
                    omember.ShowName = member.ShowName;
                    omember.QQ = member.QQ;
                    omember.Phone = member.Phone;
                    omember.RealName = member.RealName;
                    if (member.RoleID != 0)
                        omember.RoleID = member.RoleID;
                    iMember.Update(omember);
                    return RedirectToAction("Index");
                }
                return View(member);
            }
            return View("~/Views/Common/Denied.cshtml");
        }

        //
        // GET: /Member/Delete/5

        [Authorize(Roles = ("Owner,SuperAdmin"))]
        public ActionResult Delete(int id)
        {
            if (User.IsInRole("SuperAdmin") || User.IsInRole("Owner"))
            {
                Member member = iMember.GetDetail(id);
                return View(member);
            }
            return View("~/Views/Common/Denied.cshtml");
        }

        //
        // POST: /Member/Delete/5

        [HttpPost, ActionName("Delete"), Authorize(Roles = ("Owner,SuperAdmin"))]
        public ActionResult DeleteConfirmed(int id)
        {
            if (User.IsInRole("SuperAdmin") || User.IsInRole("Owner"))
            {
                iMember.Delete(id);
                return RedirectToAction("Index");
            }
            return View("~/Views/Common/Denied.cshtml");

        }


        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }


        [HttpPost]
        public ActionResult ChangePassword(string email, string password, string newpassword)
        {
            if (ModelState.IsValid)
            {
                if (iMember.ValidateLogon(email, password))
                {
                    Member entity = iMember.GetDetail(Convert.ToInt32(User.Identity.Name));
                    entity.Password = Md5Helper.MD5(newpassword, 32);
                    iMember.Update(entity);
                    FormsAuthentication.SignOut();
                    return View("~/Views/account/ChangePasswordSuccess.cshtml");
                }
                else
                {
                    ModelState.AddModelError("password", "你输入的账号或密码错误");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult IForget()
        {
            return View();
        }


        [HttpPost]
        public ActionResult IForget(string email)
        {
            if (ModelState.IsValid)
            {

                Member member = iMember.GetDetail(m => m.Email == email);
                if (member != null)
                {
                    member.Password = member.Password.Substring(0, 16);
                    member.LastVisit = DateTime.Now.AddDays(1);
                    iMember.Update(member);
                    Inform.FindPassword(member);
                    return Content(string.Format("找回的地址已发到邮箱{0},请注意在24小时内操作完毕，谢谢！", email));

                }
                else
                {
                    ModelState.AddModelError("email", "你输入的邮箱没有注册");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult NewPassword(string id)
        {

            ViewData["code"] = id;
            return View();


        }


        [HttpPost]
        public ActionResult NewPassword(string code, string password)
        {
            if (ModelState.IsValid)
            {
                Member member = iMember.GetDetail(m => m.Password == code);
                if (member != null)
                {
                    if (member.LastVisit < DateTime.Now)
                        return Content("地址过期，请重新找回");
                    else
                    {
                        member.Password = Md5Helper.MD5(password, 32);
                        iMember.Update(member);
                        FormsAuthentication.SignOut();
                        return Content("更新完成，请重新登录");
                    }

                }
                else
                {
                    return Content("地址过期或无效，请重新找回");
                }

            }
            else
            {
                ModelState.AddModelError("password", "请确认地址正确");
            }
            return View();
        }






        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("index", "home");
        }
        public ActionResult Logon()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Logon(string email, string password,string returnurl)
        {


            if (iMember.ValidateLogon(email, password))
            {
                //if (Request.IsAjaxRequest())
                //    return JavaScript("window.history.go(-1);");
                //else
                    //return RedirectToAction("Index", "Home");
                if(string.IsNullOrEmpty(returnurl))
                    return RedirectToAction("Index", "Home");
                else
                    return Redirect(returnurl);
            }
            else
            {
                ModelState.AddModelError("email", "你输入的账号或密码错误，请重新登录");
            }
            return View();

        }




        public ActionResult Promote()
        {
            if (iMember.PromoteSenior())
            {
                FormsAuthentication.SignOut();
                return JavaScript("AjaxMessage('升级成功,刷新页面可见');");
            }
            return JavaScript("AjaxMessage('升级失败，节操币至少200');");
        }


        protected override void Dispose(bool disposing)
        {
            iMember.Dispose();
            base.Dispose(disposing);
        }



    }



}