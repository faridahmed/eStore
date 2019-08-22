using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineApp.Models;
using Microsoft.Reporting.WebForms;
using System.Web.UI.WebControls;
using ReportViewerForMvc;

namespace OnlineApp.Controllers
{
    [Authorize]
    public class WorkOrderController : Controller
    {
        // GET: Report
        public LIVEEntities db = new LIVEEntities();
        public ActionResult Index()
        {

            ViewBag.ReqRecID = new SelectList(db.FrdReceiveMasters, "ReqRecID", "ReqRecID");


            return View();


        }
        public ActionResult OrderCount(Nullable<int> id)
        {

            ReportViewer reportViewer = new ReportViewer
            {
                ProcessingMode = ProcessingMode.Local,
                SizeToReportContent = true,
                Width = Unit.Percentage(50),
                Height = Unit.Percentage(50)
            };


            var v = (from x in db.FrdReceiveDetails where x.ReqRecID == id select x).FirstOrDefault();
            List<Work_order_Result> obj = db.Work_order(id).ToList();
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports/workorder.rdlc";

            //ReportParameter rp2 = new ReportParameter("wName", v.OrderId.ToString());

            // reportViewer.LocalReport.SetParameters(new ReportParameter[] { rp2 });

            ReportDataSource rdc = new ReportDataSource("workorderdataset", obj);
            reportViewer.LocalReport.DataSources.Add(rdc);

            reportViewer.LocalReport.Refresh();
            reportViewer.Visible = true;

            ViewBag.ReportViewer = reportViewer;
            return PartialView("OrderCount");

        }
    }
}