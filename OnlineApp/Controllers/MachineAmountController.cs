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
    public class MachineAmountController : Controller
    {
        // GET: Report
        public LIVEEntities db = new LIVEEntities();
        public ActionResult Index()
        {

            ViewBag.PlantNo = new SelectList(db.sPlants, "PlantNo", "PlantName");


            return View();


        }
        public ActionResult Amount(Nullable<int> id)
        {

            ReportViewer reportViewer = new ReportViewer
            {
                ProcessingMode = ProcessingMode.Local,
                SizeToReportContent = true,
                Width = Unit.Percentage(50),
                Height = Unit.Percentage(50)
            };


            var v = (from x in db.sPlants where x.PlantNo == id select x).FirstOrDefault();
            List<machineWiseAmount_Result> obj = db.machineWiseAmount(id).ToList();
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports/machinewiseamount.rdlc";

            //ReportParameter rp2 = new ReportParameter("wName", v.OrderId.ToString());

            // reportViewer.LocalReport.SetParameters(new ReportParameter[] { rp2 });

            ReportDataSource rdc = new ReportDataSource("machinewiseamount", obj);
            reportViewer.LocalReport.DataSources.Add(rdc);

            reportViewer.LocalReport.Refresh();
            reportViewer.Visible = true;

            ViewBag.ReportViewer = reportViewer;
            return PartialView("Amount");

        }
    }
}