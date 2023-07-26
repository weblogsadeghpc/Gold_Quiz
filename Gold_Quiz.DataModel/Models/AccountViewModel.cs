using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gold_Quiz.DataModel.Models
{
    public class LoginViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "شماره موبایل وارد نشده است.")] //halate ejbari
        [StringLength(11, ErrorMessage = "شماره موبایل 11 رقمی می باشد.")] // tedad horof
        [RegularExpression(@"^[^\\/:*;\.\)\(]+$", ErrorMessage = "کاراکترهای غیر مجاز وارد شده است.")] // format khas shomare mobile
        public string UserName { get; set; } //Phone Number
        [Required(AllowEmptyStrings = false, ErrorMessage = "پسورد وارد نشده است.")] //halate ejbari
        public string Password { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "شماره موبایل وارد نشده است.")]
        [StringLength(11, ErrorMessage = "شماره موبایل 11 رقمی می باشد .")]
        [RegularExpression(@"^[^\\/:*;\.\)\(]+$", ErrorMessage = "کاراکترهای غیر مجاز وارد شده است.")] // format khas shomare mobile
        public string UserName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "پسورد وارد نشده است.")] //halate ejbari
        public string Password { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "تکرار پسورد وارد نشده است.")] //halate ejbari
        [Compare("Password", ErrorMessage = "پسورد ها با یکدیگر مطابقت ندارد .")]
        public string ConfirmPassword { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "نام کوچک وارد نشده است .")]
        [StringLength(maximumLength: 60, ErrorMessage = "نام حداکثر 60 کاراکتر میباشد .")]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "نام خانوادگی وارد نشده است .")]
        [StringLength(maximumLength: 250, ErrorMessage = "نام خانوادگی حداکثر 250 کاراکتر میباشد .")]
        public string Family { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "نام محل وارد نشده است .")]
        public string CustomerName { get; set; }
        public string CustomerType { get; set; }


    }

}
