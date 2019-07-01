using OnlineApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineApp.FrdData
{
    public class MyController : Controller
    {
        private LIVEEntities databaseManager = new LIVEEntities();
        public JsonResult GetProductData(int itemno)
        {
            string IDesc;

            try
            {
                using (LIVEEntities data = new LIVEEntities())
                {
                    var ii = (from x in data.FrdItems where x.ItemNo == itemno select x).FirstOrDefault();
                    IDesc = ii.ItemDescription;
                }

                return new JsonResult { Data = IDesc, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return Json(new { status = "error", message = "ItemNot Found" });
                throw ex;
            }
        }
    }
}