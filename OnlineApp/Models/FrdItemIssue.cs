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
    
    public partial class FrdItemIssue
    {
        public int PlantID { get; set; }
        public int TrNo { get; set; }
        public Nullable<System.DateTime> TrDate { get; set; }
        public Nullable<int> RefOrderNo { get; set; }
        public Nullable<int> BeneficiaryID { get; set; }
        public Nullable<int> DeptID { get; set; }
        public string Remarks { get; set; }
        public Nullable<int> ReceivedBy { get; set; }
        public string ReceiverName { get; set; }
        public string Status { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> TotQty { get; set; }
        public Nullable<decimal> TotAmt { get; set; }
    }
}
