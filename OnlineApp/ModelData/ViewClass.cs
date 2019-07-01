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
}