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
    using System.Collections.Generic;
    
    public partial class FrdStockMaster
    {
        public int WarehouseID { get; set; }
        public int ItemNo { get; set; }
        public Nullable<int> StockQty { get; set; }
        public Nullable<decimal> StockQuantity { get; set; }
        public Nullable<int> AdjustedQty { get; set; }
        public Nullable<decimal> AdjustedQuantity { get; set; }
        public Nullable<int> ReturnQty { get; set; }
        public Nullable<decimal> ReturnQuantity { get; set; }
        public Nullable<int> StartingQty { get; set; }
        public Nullable<decimal> StartingQuantity { get; set; }
        public Nullable<decimal> LastUnitPrice { get; set; }
        public Nullable<System.DateTime> LastPurchaseDate { get; set; }
        public string MovingFlag { get; set; }
        public string Active { get; set; }
    }
}
