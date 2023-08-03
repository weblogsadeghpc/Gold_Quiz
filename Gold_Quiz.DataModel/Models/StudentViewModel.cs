using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Gold_Quiz.DataModel.Models
{
    public class StudentViewModel
    {
        [Display(Name = "نام")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "نام کوچک وارد نشده است .")]
        [StringLength(maximumLength: 60, ErrorMessage = "نام حداکثر 60 کاراکتر میباشد .")]
        public string FirstName { get; set; }
        [Display(Name = "نام خانوادگی")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "نام خانوادگی وارد نشده است .")]
        [StringLength(maximumLength: 250, ErrorMessage = "نام خانوادگی حداکثر 250 کاراکتر میباشد .")]
        public string Family { get; set; }
        [Display(Name = "شماره تماس")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "شماره موبایل وارد نشده است.")]
        [StringLength(11, ErrorMessage = "شماره موبایل 11 رقمی می باشد .")]
        [RegularExpression(@"^[^\\/:*;\.\)\(]+$", ErrorMessage = "کاراکترهای غیر مجاز وارد شده است.")]
        public string UserName { get; set; }
        [Display(Name = "ایمیل")]
        [EmailAddress(ErrorMessage = " ایمیل وارد شده صحیح نمیباشد . ")]
        public string Email { get; set; }
        public string UserID { get; set; }
    }
}
