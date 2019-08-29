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
    public class RequisitionSlipController : Controller
    {
        // GET: Report
        public LIVEEntities db = new LIVEEntities();
        public ActionResult Index()
        {

            ViewBag.ReqID = new SelectList(db.FrdRequestMasters, "ReqID", "ReqID");
            ViewBag.PlantNo = new SelectList(db.sPlants, "PlantNo", "PlantName");
            ViewBag.ReqRecID = new SelectList(db.FrdReceiveMasters, "ReqRecID", "ReqRecID");


            return View();


        }
        //requisition
        public ActionResult RequisitionslipCount(Nullable<int> reqid, Nullable<int> planid)
        {

            ReportViewer reportViewer = new ReportViewer
            {
                ProcessingMode = ProcessingMode.Local,
                SizeToReportContent = true,
                Width = Unit.Percentage(50),
                Height = Unit.Percentage(50)
            };


            //var v = (from x in db.FrdRequestMasters where x.ReqID == reqid select x).FirstOrDefault();
            List<requisitionSlip_Result> obj = db.requisitionSlip(reqid,planid).ToList();
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports/requisitionslip.rdlc";

            //ReportParameter rp2 = new ReportParameter("wName", v.OrderId.ToString());

            // reportViewer.LocalReport.SetParameters(new ReportParameter[] { rp2 });

            ReportDataSource rdc = new ReportDataSource("requisitionslip", obj);
            reportViewer.LocalReport.DataSources.Add(rdc);

            reportViewer.LocalReport.Refresh();
            reportViewer.Visible = true;

            ViewBag.ReportViewer = reportViewer;
            return PartialView("RequisitionslipCount");

        }

        //Quality Assurance check
        public ActionResult AssuranceCheck(Nullable<int> reqrecid, Nullable<int> planid)
        {

            ReportViewer reportViewer = new ReportViewer
            {
                ProcessingMode = ProcessingMode.Local,
                SizeToReportContent = true,
                Width = Unit.Percentage(50),
                Height = Unit.Percentage(50)
            };


            //var v = (from x in db.FrdRequestMasters where x.ReqID == reqid select x).FirstOrDefault();
            List<qualityAssurance_Result> obj = db.qualityAssurance(planid, reqrecid).ToList();
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports/qualityAssurance.rdlc";

            //ReportParameter rp2 = new ReportParameter("wName", v.OrderId.ToString());

            // reportViewer.LocalReport.SetParameters(new ReportParameter[] { rp2 });

            ReportDataSource rdc = new ReportDataSource("qualityassure", obj);
            reportViewer.LocalReport.DataSources.Add(rdc);

            reportViewer.LocalReport.Refresh();
            reportViewer.Visible = true;

            ViewBag.ReportViewer = reportViewer;
            return PartialView("AssuranceCheck");

        }

        //Stock and Receieve Quantity
        public ActionResult IssueRcvCount(DateTime d1, DateTime d2, Nullable<int> planid)
        {

            ReportViewer reportViewer = new ReportViewer
            {
                ProcessingMode = ProcessingMode.Local,
                SizeToReportContent = true,
                Width = Unit.Percentage(50),
                Height = Unit.Percentage(50)
            };


            //var v = (from x in db.FrdRequestMasters where x.ReqID == reqid select x).FirstOrDefault();
            List<IssueReturnQty_Result> obj = db.IssueReturnQty(planid,d1,d2).ToList();
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports/issuereturnqty.rdlc";

            ReportParameter rp1 = new ReportParameter("d1", d1.ToString("dd-MMM-yy") + " to " + d2.ToString("dd-MMM-yy"));
            ReportParameter rp2 = new ReportParameter("planid", planid.ToString());

            reportViewer.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2 });
            ReportDataSource rdc = new ReportDataSource("issuercvdataset", obj);
            reportViewer.LocalReport.DataSources.Add(rdc);

            reportViewer.LocalReport.Refresh();
            reportViewer.Visible = true;

            ViewBag.ReportViewer = reportViewer;
            return PartialView("RequisitionslipCount");

        }
    }
}