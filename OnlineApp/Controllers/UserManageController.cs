using OnlineApp.ModelData;
using OnlineApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Validation;

namespace OnlineApp.Controllers
{
    [Authorize]
    public class UserManageController : Controller
    {
        public LIVEEntities db = new LIVEEntities();
        // GET: UserManage
        public ActionResult Index()
        {
            LIVEEntities db = new LIVEEntities();
            List<sRole> role = db.sRoles.ToList();
            ViewBag.RoleID = new SelectList(db.sRoles, "RoleID", "RoleName");
            var data = db.sPlants.Select(s => new
            {
                Text = s.PlantNo + "---" + s.PlantCode + " --- " + s.PlantName,
                Value = s.PlantNo
            }).ToList();
            ViewBag.PlantNo = new SelectList(data, "Value", "Text");
            int value = db.sUsers.Max(a => a.UserID);
            int num = value;

            sUser usr = new sUser
            {
                UserID = (num + 1),

            };

            return View(usr);
        }
        [HttpPost]
        public ActionResult CreateUser(UserReqVM user)
        {
            using (LIVEEntities db = new LIVEEntities())
            {
                sUser nw = new sUser();
                nw.UserID = user.UserID;
                nw.UserName = user.UserName;
                nw.UserPass = user.UserPass;
                nw.UserPin = user.UserPin;
                nw.RoleId = user.RoleId;
                nw.MobileNo = user.MobileNo;
                nw.PlantNo = user.PlantNo;
                nw.Email = user.Email;
                nw.UserStatus = "ACT";
                nw.LoginType = "A";
                nw.ActiveSession = 0;
                nw.CreateBy = Session["Name"].ToString();
                nw.CreateDate = DateTime.Now;
                db.sUsers.Add(nw);
                db.SaveChanges();
                return RedirectToAction("Index");

            }



        }
        public ActionResult UserRight()
        {
            LIVEEntities db = new LIVEEntities();
            ViewBag.UserID = new SelectList(db.sUsers, "UserID", "UserName");
            //ViewBag.PageType = new SelectList(db.sPageNames, "PageType", "PageType");
            var tCode = new SelectList(
         new[]
         {
                       new { ID = 2, Name = "Reports" },
                       new { ID = 1, Name = "Forms" },
              },
         "ID",
         "Name"
     );
            ViewBag.PageType = tCode;
            return View("UserRight");
        }
        [HttpGet]
        public JsonResult GetUserInfo(int pUserID)
        {

            try
            {

                var userinfo = db.sUsers.Where(e => e.UserID == pUserID);
                return new JsonResult { Data = userinfo, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return Json(new { status = "error", message = "No data found for the user..!!" });
                throw ex;
            }
        }
        [HttpGet]
        public JsonResult GetMenuList(int inType)
        {
            using (LIVEEntities db = new LIVEEntities())
            {
                try
                {

                    var MenuData = db.sPageNames.Where(x => x.PageType == inType).ToList();
                    //var xv = MenuData.Count();
                    return new JsonResult { Data = MenuData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
                catch (Exception ex)
                {
                    return Json(new { status = "error", message = "Not Assign" });
                    throw ex;
                }
            }
        }
    }
}