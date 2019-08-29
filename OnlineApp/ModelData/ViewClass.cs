using OnlineApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineApp.ModelData
{
    public class ViewClass
    {
    }
    public class ConItem
    {
        public int Id { get; set; }
        public int ItemNo { get; set; }
        public int ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public Nullable<int> ItemType { get; set; }
        public Nullable<int> ItemTypeCode { get; set; }
        public Nullable<int> ItemUnitCode { get; set; }
        public int PlantCode { get; set; }
        public Nullable<int> ItemMachineCode { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public string TaxFlag { get; set; }
        public int ConvertValue { get; set; }
        public string UseFor { get; set; }
        public string Show { get; set; }
  

    }
    public  class RequestVM
    {
        public int PlantID { get; set; }
        public int ReqID { get; set; }
        public Nullable<System.DateTime> ReqDate { get; set; }
        public string RefNo { get; set; }
        public string Remarks { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public Nullable<int> DeptID { get; set; }
        public string Status { get; set; }
        //  public string CreateBy { get; set; }
        // public Nullable<System.DateTime> CreateDate { get; set; }
        public List<FrdRequestDetail> reqdtl { get; set; }
    }
    public class UserReqVM
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        [Required(ErrorMessage = "User Name is Required.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is Required.")]
        public string UserPass { get; set; }
        public string UserPin { get; set; }
        public Nullable<int> RoleId { get; set; }
        public string MobileNo { get; set; }
        public string UserStatus { get; set; }
        public Nullable<int> PlantNo { get; set; }
        [Required(ErrorMessage = "Email is Required.")]
        public string Email { get; set; }
        public string LoginType { get; set; }
        public Nullable<int> ActiveSession { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
    }
    public class ItemVM
    {
        public int Id { get; set; }
        public int ItemNo { get; set; }
        public int ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public Nullable<int> ItemType { get; set; }
        public Nullable<int> ItemTypeCode { get; set; }
        public Nullable<int> ItemUnitCode { get; set; }
        public int PlantCode { get; set; }
        public Nullable<int> ItemMachineCode { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public string TaxFlag { get; set; }
        public int ConvertValue { get; set; }
        public string UseFor { get; set; }
        public string Show { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
    }
    public class ReceiveVM
    {
        public int PlantID { get; set; }
        public int ReqRecID { get; set; }
        public Nullable<int> SupplierID { get; set; }
        public Nullable<int> ReqID { get; set; }
        public Nullable<System.DateTime> RecDate { get; set; }
        public Nullable<int> TypeCode { get; set; }
        public string UserNote { get; set; }
        public string AppBy { get; set; }
        public int DeptID { get; set; }
        //public Nullable<System.DateTime> AppDate { get; set; }
        public string AppFlag { get; set; }
        //public string AppRemarks { get; set; }
        //public string AppBy2 { get; set; }
        //public Nullable<System.DateTime> AppDate2 { get; set; }
        public string AppFlag2 { get; set; }
        public string AppRemarks2 { get; set; }
       // public string CreateBy { get; set; }
       // public Nullable<System.DateTime> CreateDate { get; set; }
        public List<FrdReceiveDetail> itemdtl { get; set; }
    }
    public  class ApprovalVM
    {
        public int PlantID { get; set; }
        public int RefNo { get; set; }
        public int AppID { get; set; }
        public string AppCode { get; set; }
        public int AppType { get; set; }
        public string FirstApp { get; set; }
        public string SecondApp { get; set; }
        public string FirstStatus { get; set; }
        public string SecondStatus { get; set; }
        public Nullable<System.DateTime> FirstDate { get; set; }
        public Nullable<System.DateTime> SecondDate { get; set; }
        public string FirstRemarks { get; set; }
        public string SecondRemarks { get; set; }
    }
    public  class TransactionVM
    {
        public int PlantID { get; set; }
        public int ReceivedTranNo { get; set; }
        public Nullable<int> LocalForeign { get; set; }
        public Nullable<System.DateTime> TranDate { get; set; }
        public Nullable<int> RefOrderNo { get; set; }
        public Nullable<int> RefInvoice { get; set; }
        public Nullable<int> SupplierID { get; set; }
        public string IndentNo { get; set; }
        public Nullable<int> LCNo { get; set; }
        public Nullable<decimal> TotalQty { get; set; }
        public Nullable<decimal> TotalPrice { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        // public string CreateBy { get; set; }
        // public Nullable<System.DateTime> CreateDate { get; set; }
        public List<FrdPurchaseInfo> reqdtl { get; set; }
    }
    public class IssueVm
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
        //public string CreateBy { get; set; }
        //public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> TotQty { get; set; }
        public Nullable<decimal> TotAmt { get; set; }
       

    }
    public class TranVM
    {
        public int PlantID { get; set; }
        public Nullable<System.DateTime> TrDate { get; set; }
        public int TrNo { get; set; }
        public Nullable<int> TranType { get; set; }
        public Nullable<int> RefNo { get; set; }
        public string Remarks { get; set; }
        //public string CreateBy { get; set; }
        //public Nullable<System.DateTime> CreateDate { get; set; }
        public List<FrdItemTranInfo> itemdtl { get; set; }

    }
}