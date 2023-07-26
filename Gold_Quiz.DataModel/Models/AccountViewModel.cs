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
}
