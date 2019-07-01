using System.ComponentModel.DataAnnotations;
namespace OnlineApp.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public int UserID
        {
            get;
            set;
        }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password
        {
            get;
            set;
        }
    }
}
