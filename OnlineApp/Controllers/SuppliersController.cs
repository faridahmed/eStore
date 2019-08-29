using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineApp.Models;
using System.Data.Entity.Validation;

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
            int value = db.FrdSuppliers.Max(a => a.SupplierID);
            int num = value;

            FrdSupplier sup = new FrdSupplier
            {
                SupplierID = (num + 1),

            };

            return View(sup);
        }

        // POST: Suppliers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       
        public ActionResult Create(FrdSupplier frdSupplier)
        {
            LIVEEntities db = new LIVEEntities();
            try
            {
                if (ModelState.IsValid)
                    {
                FrdSupplier sp = new FrdSupplier();
                    sp.SupplierID = frdSupplier.SupplierID;
                sp.SupplierCode = frdSupplier.SupplierCode;
                sp.SupplierName = frdSupplier.SupplierName;
                sp.SupplierAddress = frdSupplier.SupplierAddress;
                sp.PhoneNumber = frdSupplier.PhoneNumber;
                sp.MobileNumber = frdSupplier.MobileNumber;
                sp.EmailAddress = frdSupplier.EmailAddress;
                sp.ZoneCode = frdSupplier.ZoneCode;
                sp.BranchCode = frdSupplier.BranchCode;
                sp.Remarks = frdSupplier.Remarks;
                sp.VATRegNo = "222";
                sp.Status = "ACT";
                db.FrdSuppliers.Add(sp);
                db.SaveChanges();
                    }

            }
            
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            return RedirectToAction("Index");


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
