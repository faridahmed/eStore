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
    public class StoreTransactionController : Controller
    {
        private LIVEEntities databaseManager = new LIVEEntities();
        // GET: StoreTransaction
        public ActionResult ItemReceived()
        {
            var w = (from y in databaseManager.sUsers
                     where y.UserID.ToString() == User.Identity.Name
                     select new { y.PlantNo }).FirstOrDefault();
            var wn = databaseManager.sPlants.Where(x => x.PlantNo == w.PlantNo).FirstOrDefault();
            ViewBag.WarehouseID = wn.PlantNo;
            ViewBag.WarehouseIDLogin = wn.PlantName;
            ViewBag.RefInvoice = new SelectList((from s in databaseManager.FrdReceiveMasters
                                               join cust in databaseManager.FrdSuppliers
                                                      on s.SupplierID equals cust.SupplierID
                                               where s.PlantID == cust.BranchCode && s.PlantID == w.PlantNo && s.AppFlag == "A" && s.AppBy != "XXXXX" && s.AppFlag2 == "A"
                                               orderby s.ReqRecID descending
                                               select new
                                               {
                                                   ReqRecID = s.ReqRecID,
                                                   CustomerID = cust.SupplierName + " (" + s.ReqRecID + ") " + s.ReqRecID
                                               }),
           "ReqRecID", "CustomerID", null);
            var tCode = new SelectList(
            new[]
                {
                       new { ID = 130, Name = "Local" },
                       new { ID = 120, Name = "Foreign" },
                },
                "ID",
                "Name"
            );
            ViewBag.TypeCode = tCode;
            var tCodes = new SelectList(
            new[]
                {
                       new { ID = 220, Name = "Sphere" },
                       new { ID = 230, Name = "Non-Sphere" },
                },
                "ID",
                "Name"
            );
            ViewBag.TypeCodes = tCodes;
            return View("ItemReceived");
        }
         public JsonResult ShowData(int inWarehouseID, int inTrNo)
        {
            var pData = databaseManager.spPurchaseData(inWarehouseID, inTrNo, 100);
            return new JsonResult { Data = pData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        public ActionResult PurApproved(TransactionVM D)
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
            var maxreqno = (from n in databaseManager.FrdApprovals where n.PlantID == w.PlantNo select n.AppID).DefaultIfEmpty(reqno).Max();
            var maxrNo = maxreqno + 1;
            string Code = string.Concat("APP" + maxreqno + "RM");
            int v = maxrNo;
            try
            {
                // using (LIVEEntities ddd = new LIVEEntities())
                using (var transaction = databaseManager.Database.BeginTransaction())
                {
                    if (ModelState.IsValid)
                    {
                        FrdApproval dbo = new FrdApproval();
                        {
                            dbo.AppID = maxrNo;
                            dbo.PlantID = D.PlantID;
                            //dbo.RefNo = D.RefNo;
                            dbo.AppType = 1;
                            dbo.AppCode = Code;
                            //dbo.FirstApp = D.ReqID;
                            //dbo.SecondRemarks = D.SecondRemarks;
                            dbo.SecondStatus = "A";
                            //dbo.RefNo = D.RefNo;
                            //dbo.SupplierID = D.SupplierID;
                            //dbo.UserNote = D.UserNote;
                            //dbo.TypeCode = D.TypeCode;
                            //  if (D.TypeCode == 20)
                            // {
                            // dbo.AppBy = "XXXXX";
                            //  dbo.AppFlag = "O";
                            // }
                            //  else
                            // {
                            // dbo.AppBy = "NA";
                            // dbo.AppFlag = "N";
                            //  }
                            dbo.SecondDate = DateTime.Now;
                            dbo.SecondApp = User.Identity.Name;
                        };
                        databaseManager.FrdApprovals.Add(dbo);
                        var result = databaseManager.FrdReceiveMasters.SingleOrDefault(b => b.ReqRecID == D.RefInvoice);
                        if (result != null)
                        {
                            result.AppFlag2 = "A";
                            result.AppDate2 = DateTime.Today;
                            result.AppBy2 = User.Identity.Name;
                           // result.AppRemarks2 = D.SecondRemarks;
                            databaseManager.SaveChanges();
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
                    return new JsonResult { Data = new { status = status, mes = mes, v = v } };
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