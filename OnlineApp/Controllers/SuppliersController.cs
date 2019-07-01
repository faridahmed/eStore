using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineApp.Models;

namespace OnlineApp.Controllers
{
    public class SuppliersController : Controller
    {
        private LIVEEntities db = new LIVEEntities();

        // GET: Suppliers
        public ActionResult Index()
        {
            return View(db.FrdSuppliers.ToList());
        }

        // GET: Suppliers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FrdSupplier frdSupplier = db.FrdSuppliers.Find(id);
            if (frdSupplier == null)
            {
                return HttpNotFound();
            }
            return View(frdSupplier);
        }

        // GET: Suppliers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SupplierID,SupplierCode,SupplierName,SupplierAddress,PhoneNumber,MobileNumber,EmailAddress,Status,ZoneCode,BranchCode,MultiFlag,Remarks,ContactPerson")] FrdSupplier frdSupplier)
        {
            if (ModelState.IsValid)
            {
                db.FrdSuppliers.Add(frdSupplier);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(frdSupplier);
        }

        // GET: Suppliers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FrdSupplier frdSupplier = db.FrdSuppliers.Find(id);
            if (frdSupplier == null)
            {
                return HttpNotFound();
            }
            return View(frdSupplier);
        }

        // POST: Suppliers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SupplierID,SupplierCode,SupplierName,SupplierAddress,PhoneNumber,MobileNumber,EmailAddress,Status,ZoneCode,BranchCode,MultiFlag,Remarks,ContactPerson")] FrdSupplier frdSupplier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(frdSupplier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(frdSupplier);
        }

        // GET: Suppliers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FrdSupplier frdSupplier = db.FrdSuppliers.Find(id);
            if (frdSupplier == null)
            {
                return HttpNotFound();
            }
            return View(frdSupplier);
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FrdSupplier frdSupplier = db.FrdSuppliers.Find(id);
            db.FrdSuppliers.Remove(frdSupplier);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
