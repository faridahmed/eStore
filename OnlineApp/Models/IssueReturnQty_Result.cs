//------------------------------------------------------------------------------
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
    
    public partial class IssueReturnQty_Result
    {
        public int WarehouseID { get; set; }
        public string WarehouseName { get; set; }
        public int TrNo { get; set; }
        public Nullable<System.DateTime> TrDate { get; set; }
        public Nullable<int> OrderNo { get; set; }
        public int ItemNo { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public int ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemDes { get; set; }
        public string BenName { get; set; }
        public Nullable<int> IssQty { get; set; }
        public Nullable<decimal> RecQty { get; set; }
        public int OrderQty { get; set; }
    }
}
