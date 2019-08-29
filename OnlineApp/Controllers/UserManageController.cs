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
        private LIVEEntities databaseManager = new LIVEEntities();
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
                nw.CreateBy = User.Identity.Name;
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
        public ActionResult ChangePass()
        {
            var w = (from y in db.sUsers
                     where y.UserID.ToString() == User.Identity.Name
                     select new { y.UserID }).FirstOrDefault();
            var un = (from y in db.sUsers
                     where y.UserID.ToString() == User.Identity.Name
                     select new { y.UserName }).FirstOrDefault();
            ViewBag.userID = w.UserID;
            ViewBag.userName = un.UserName;
            return View("ChangePass");
        }
        [HttpGet]
        public JsonResult GetUserPass(int pUserID)
        {
            string pass;

            try
            {

                var userinfo = db.sUsers.Where(e => e.UserID == pUserID).FirstOrDefault();
                pass = userinfo.UserPass;
                return new JsonResult { Data = pass, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return Json(new { status = "error", message = "Password not found for the user..!!" });
                throw ex;
            }
        }
        public ActionResult SaveUpdate(ChangeVM D)
        {
            bool status = false;
            string mes = "";
            var w = (from y in databaseManager.sUsers
                     where y.UserID.ToString() == User.Identity.Name
                     select new { y.UserID }).FirstOrDefault();
            try
            {
                using (var transaction = databaseManager.Database.BeginTransaction())
                {
                    if (ModelState.IsValid)
                    {
                        var result = databaseManager.sUsers.SingleOrDefault(b => b.UserID == w.UserID);
                        if (result != null)
                        {
                            result.UserPass = D.NewPass ;
                            result.UserPin = D.UserPass;                      
                        }
                        databaseManager.SaveChanges();
                        transaction.Commit();
                        status = true;
                        databaseManager.Dispose();
                        ModelState.Clear();
                    }
                    else
                    {
                        status = false;
                        transaction.Rollback();

                    }
                    return new JsonResult { Data = new { status = status, mes = mes } };
                }
            }
            catch (Exception ex)
            {
                string mess = ex.Message;
                return Json(new { status = "error", message = "Error Generate" });
            }
        }
    }
}