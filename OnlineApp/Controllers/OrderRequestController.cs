using OnlineApp.FrdData;
using OnlineApp.ModelData;
using OnlineApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineApp.Controllers
{
    [Authorize]
    public class OrderRequestController : Controller
    {

        private LIVEEntities databaseManager = new LIVEEntities();
        // GET: OrderRequest
        public ActionResult OrderGen()
        {
            var w = (from y in databaseManager.sUsers
                     where y.UserID.ToString() == User.Identity.Name
                     select new { y.PlantNo }).FirstOrDefault();
            var wn = databaseManager.sPlants.Where(x => x.PlantNo == w.PlantNo).FirstOrDefault();
            var cust = databaseManager.sBenificiaries.Where(x => x.PlantID == wn.PlantNo && x.Status == "Y").Select(x => new { Text = x.BenificiaryName + " , " + x.BenificiaryID, Value = x.BenificiaryID }).OrderBy(e => e.Text).ToList();
            ViewBag.WarehouseID = wn.PlantNo;
            ViewBag.WarehouseIDLogin = wn.PlantName;
            ViewBag.CustomerID = new SelectList(cust, "Value", "Text");
            //ViewBag.DeptID = databaseManager.sDepts.OrderBy(b => b.DeptName);
            ViewBag.DeptID = new SelectList(databaseManager.sDepts.OrderBy(x => x.DeptName), "DeptID", "DeptName");
            return View("OrderGen");
        }

        [HttpGet]
        public JsonResult getItemDescNo(int inCustomer)
        {
            var data = databaseManager.FrdItems.Where(c => c.PlantCode == inCustomer && c.Show == "N").OrderBy(c => c.ItemDescription).Select(c => new { id = c.ItemNo, name = c.ItemDescription + " , " + c.ItemCode }).ToList();
            return Json(data, "data", JsonRequestBehavior.AllowGet);
        }
        public JsonResult getItemDesc(int inCustomer)
        {
            string idesc;
            MyController mc = new MyController();
            var dd = databaseManager.FrdItems.Where(zz => zz.ItemNo == inCustomer).FirstOrDefault();
            idesc = dd.ItemDescription;
            return new JsonResult { Data = idesc, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        [HttpPost]
        public ActionResult SaveData(RequestVM D)
        {
            bool status = false;
            string mes = "";
            var w = (from y in databaseManager.sUsers
                     where y.UserID.ToString() == User.Identity.Name
                     select new { y.PlantNo }).FirstOrDefault();
            var wn = databaseManager.sPlants.Where(x => x.PlantNo == w.PlantNo).FirstOrDefault();
            string s1 = w.PlantNo.ToString();
            string s2 = string.Concat(s1 + "000000");
            int reqno = Convert.ToInt32(s2);
            var maxreqno = (from n in databaseManager.FrdRequestMasters where n.PlantID == w.PlantNo select n.ReqID).DefaultIfEmpty(reqno).Max();
            var maxrNo = maxreqno + 1;
            int v = maxrNo;
            //var maxprodtr = (from j in db.ProductTransactions where j.WarehouseID == w.WorkStationID select j.TransactionNo).Max() + 1;
            using (LIVEEntities ddd = new LIVEEntities())
            {
                // CommonChk cm = new CommonChk();
                FrdRequestMaster dbo = new FrdRequestMaster();
                {
                    dbo.ReqID = maxrNo;
                    dbo.PlantID = D.PlantID;
                    dbo.ReqDate = DateTime.Today;
                    dbo.CustomerID = Convert.ToInt32(User.Identity.Name);
                    dbo.RefNo = "";
                    dbo.Remarks = D.Remarks;
                    dbo.CreateDate = DateTime.Now;
                    dbo.CreateBy = User.Identity.Name;
                };
                ddd.FrdRequestMasters.Add(dbo);
                foreach (var i in D.reqdtl)
                {
                    FrdRequestDetail obd = new FrdRequestDetail();
                    {
                        obd.PlantID = D.PlantID;
                        obd.ReqID = maxrNo;
                        obd.ItemID = i.ItemID;
                        obd.Quantity = i.Quantity;
                        obd.UnitPrice = 0;
                        ddd.FrdRequestDetails.Add(obd);
                    }

                    ddd.SaveChanges();
                    status = true;
                    ddd.Dispose();
                }
                ModelState.Clear();

            }
            return new JsonResult { Data = new { status = status, mes = mes, v = v } };
        }
    }

}