using OnlineApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineApp.ModelData
{
    public class LoginData
    {
        public int UserID { get; set; }
        [Required(ErrorMessage = "Please enter the User ID")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please enter the Password")]
        [DataType(DataType.Password)]
        public string UserPass { get; set; }
        public int? RoleId { get; set; }
        public string RoleName { get; set; }
    }
    public class MenuData
    {

        public string MenuName { get; set; }
        public int MenuID { get; set; }
        public string PageDescription { get; set; }
        public int PageCode { get; set; }
        public string ContName { get; set; }
        public string PageName { get; set; }
        public int? RoleId { get; set; }
        public string RoleName { get; set; }
        public int UserID { get; set; }
    }
    public class LoginVM
    {
        [Required]
        [Display(Name = "User ID")]
        public int UserID { get; set; }   
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string UserPass { get; set; }

    }
    public class ChangeVM
    {
        [Required]
        [Display(Name = "User ID")]
        public int UserID { get; set; }
      //  [Required]
        [DataType(DataType.Password)]
        
        [Display(Name = "Password")]
        public string UserPass { get; set; }
       // [Required]
        [DataType(DataType.Password)]
       // [MinLength(6)]
        [Display(Name = "Password")]
        public string NewPass { get; set; }
        //[Required]
        [DataType(DataType.Password)]
       // [MinLength(6)]
        [Display(Name = "Password")]
        public string ConPass { get; set; }

    }
    public class MenuSubVM
    {
        public int Id { get; set; }
        public string SubMenu { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public Nullable<int> MenuID { get; set; }
        public Nullable<int> RoleId { get; set; }
        public Nullable<int> userID { get; set; }
        public int WarehouseID { get; set; }
        public int MenuCode { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string Status { get; set; }

        public virtual sMenu MenuMain { get; set; }
        public virtual sRole MenuRole { get; set; }
        // public bool IsSelected { get; set; }
        public List<menudetailVM> menudetail { get; set; }
    }
    public class menudetailVM
    {
        public int Id { get; set; }
        public string SubMenu { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public int MainMenuId { get; set; }
        public int RoleId { get; set; }
        public int userID { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string Status { get; set; }


        public int IsSelected { get; set; }

        public int MenuCode { get; set; }




    }
    public class MenuList
    {
        public int SLNo { get; set; }
        public string FileName { get; set; }
        public string ContName { get; set; }
        public string PageName { get; set; }
        public string Status { get; set; }
        public string Used { get; set; }
        public Nullable<int> MenuID { get; set; }
        public Nullable<int> PageType { get; set; }

        public virtual sMenu sMenu { get; set; }
    }

}