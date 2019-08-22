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
                                               where s.PlantID == cust.BranchCode && s.PlantID == w.PlantNo && s.AppFlag == "A" && s.AppBy != "XXXXX" && s.Status == "PEN"
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
        public ActionResult MyTransaction(TransactionVM D)
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
            var maxreqno = (from n in databaseManager.FrdPurchases where n.PlantID == w.PlantNo select n.ReceivedTranNo).DefaultIfEmpty(reqno).Max();
            var maxrNo = maxreqno + 1;
            string Code = string.Concat("REC" + maxreqno);
            int v = maxrNo;
            try
            {             
                using (var transaction = databaseManager.Database.BeginTransaction())
                {
                    if (ModelState.IsValid)
                    {
                        FrdPurchase dbo = new FrdPurchase();
                        {
                            dbo.ReceivedTranNo = maxrNo;
                            dbo.PlantID = D.PlantID;
                            dbo.ReceivedTranNo = maxrNo;
                            dbo.TranDate = DateTime.Today;
                            dbo.RefOrderNo = D.RefOrderNo;
                            dbo.LCNo = D.LCNo;
                            dbo.LocalForeign = D.LocalForeign;
                            dbo.SupplierID = D.SupplierID;
                            dbo.IndentNo = D.IndentNo;
                            dbo.TotalQty = D.TotalQty;
                            dbo.TotalPrice = D.TotalPrice;
                            dbo.Remarks = D.Remarks;             
                            dbo.Status ="REC";
                            dbo.RefInvoice = D.RefInvoice;
                            dbo.CreateBy = User.Identity.Name;
                            dbo.CreateDate = DateTime.Now;                      
                        };
                        databaseManager.FrdPurchases.Add(dbo);
                        foreach (var i in D.reqdtl)
                        {
                            FrdPurchaseInfo obd = new FrdPurchaseInfo();
                            {
                                obd.PlantID = D.PlantID;
                                obd.TranRefNo = maxrNo;
                                obd.ItemCode = i.ItemCode;
                                obd.Qty = i.Qty;
                                obd.UnitPrice =i.UnitPrice;
                                databaseManager.FrdPurchaseInfoes.Add(obd);
                            }
                            if (i.ItemCode != 0 && D.PlantID!=0)
                            {
                                databaseManager.spStockProduct(D.PlantID, i.ItemCode, 1, Convert.ToInt32(i.Qty), i.Qty, i.UnitPrice);
                            }
                        }
                        var result = databaseManager.FrdReceiveMasters.SingleOrDefault(b => b.ReqRecID == D.RefOrderNo);
                        if (result != null)
                        {
                            result.Status = "COM";
                            //databaseManager.SaveChanges();
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
        public ActionResult ItemAdj()
        {
            var w = (from y in databaseManager.sUsers
                     where y.UserID.ToString() == User.Identity.Name
                     select new { y.PlantNo }).FirstOrDefault();
            var wn = databaseManager.sPlants.Where(x => x.PlantNo == w.PlantNo).FirstOrDefault();
            ViewBag.WarehouseID = wn.PlantNo;
            ViewBag.WarehouseIDLogin = wn.PlantName;
            ViewBag.RefOrderNo = new SelectList((from s in databaseManager.FrdRequestMasters
                                                 join cust in databaseManager.sBenificiaries
                                                        on s.CustomerID equals cust.BenificiaryID
                                                 where s.PlantID == w.PlantNo && s.Status == "A" 
                                                 orderby s.ReqID descending
                                                 select new
                                                 {
                                                     ReqRecID = s.ReqID,
                                                     CustomerID = cust.BenificiaryName + " (" + s.ReqID + ") " 
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
            return View("ItemAdj");
        }
        public ActionResult SaveAdj(IssueVm D)
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
            var maxreqno = (from n in databaseManager.FrdItemIssues where n.PlantID == w.PlantNo select n.TrNo).DefaultIfEmpty(reqno).Max();
            var maxrNo = maxreqno + 1;
            int v = maxrNo;
            try
            {
                using (var transaction = databaseManager.Database.BeginTransaction())
                {
                    if (ModelState.IsValid)
                    {
                        FrdItemIssue dbo = new FrdItemIssue();
                        {
                            dbo.TrNo = maxrNo;
                            dbo.PlantID = D.PlantID;
                            dbo.DeptID = D.DeptID;
                            dbo.TrDate = DateTime.Today;
                            dbo.BeneficiaryID = Convert.ToInt32(User.Identity.Name);
                            dbo.RefOrderNo = D.RefOrderNo;
                            dbo.Remarks = D.Remarks;
                            dbo.ReceivedBy = D.ReceivedBy;
                            dbo.ReceiverName = D.ReceiverName;
                            dbo.Status = "ISS";
                            dbo.CreateDate = DateTime.Now;
                            dbo.CreateBy = User.Identity.Name;
                        };
                        databaseManager.FrdItemIssues.Add(dbo);
                        foreach (var i in D.itemdtl)
                        {
                            FrdIssueDetail obd = new FrdIssueDetail();
                            {
                                obd.PlantID = D.PlantID;
                                obd.TrNo = maxrNo;
                                obd.ItemNo = i.ItemNo;
                                obd.Qty = i.Qty;
                                obd.DelQty = i.DelQty;
                                obd.UnitPrice = 0;
                                databaseManager.FrdIssueDetails.Add(obd);
                            }
                            if (i.ItemNo != 0 && D.PlantID != 0)
                            {
                                databaseManager.spStockProduct(D.PlantID, i.ItemNo, 2, Convert.ToInt32(i.Qty), i.Qty, 0);
                            }
                        }
                        var result = databaseManager.FrdRequestMasters.SingleOrDefault(b => b.ReqID == D.RefOrderNo);
                        if (result != null)
                        {
                            result.Status = "D";
                            //databaseManager.SaveChanges();
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