﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineApp.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class LIVEEntities : DbContext
    {
        public LIVEEntities()
            : base("name=LIVEEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<sUser> sUsers { get; set; }
        public virtual DbSet<sMenu> sMenus { get; set; }
        public virtual DbSet<sPlant> sPlants { get; set; }
        public virtual DbSet<sRole> sRoles { get; set; }
        public virtual DbSet<sUserPage> sUserPages { get; set; }
        public virtual DbSet<FrdMachine> FrdMachines { get; set; }
        public virtual DbSet<FrdParameter> FrdParameters { get; set; }
        public virtual DbSet<sDocument> sDocuments { get; set; }
        public virtual DbSet<FrdSupplier> FrdSuppliers { get; set; }
        public virtual DbSet<FrdStockMaster> FrdStockMasters { get; set; }
        public virtual DbSet<FrdItem> FrdItems { get; set; }
        public virtual DbSet<FrdRequestDetail> FrdRequestDetails { get; set; }
        public virtual DbSet<FrdRequestMaster> FrdRequestMasters { get; set; }
        public virtual DbSet<sBenificiary> sBenificiaries { get; set; }
        public virtual DbSet<sDesignation> sDesignations { get; set; }
        public virtual DbSet<sUnit> sUnits { get; set; }
        public virtual DbSet<FrdApproval> FrdApprovals { get; set; }
        public virtual DbSet<FrdReceiveDetail> FrdReceiveDetails { get; set; }
        public virtual DbSet<FrdReceiveMaster> FrdReceiveMasters { get; set; }
        public virtual DbSet<sPageName> sPageNames { get; set; }
        public virtual DbSet<sDept> sDepts { get; set; }
    
        public virtual ObjectResult<spUserLoginToApplication_Result> spUserLoginToApplication(Nullable<int> userID, string userPass, string userPin)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var userPassParameter = userPass != null ?
                new ObjectParameter("UserPass", userPass) :
                new ObjectParameter("UserPass", typeof(string));
    
            var userPinParameter = userPin != null ?
                new ObjectParameter("UserPin", userPin) :
                new ObjectParameter("UserPin", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spUserLoginToApplication_Result>("spUserLoginToApplication", userIDParameter, userPassParameter, userPinParameter);
        }
    }
}
